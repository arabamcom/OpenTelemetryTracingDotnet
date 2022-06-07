using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace OpenTelemetryExample.Core.Helpers
{
	public class JsonHelper
	{
		public static string SerializeWithIgnoreRelations(object model)
		{
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				ContractResolver = new CustomResolver(),
				PreserveReferencesHandling = PreserveReferencesHandling.None,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				Formatting = Formatting.None
			};

			return JsonConvert.SerializeObject(model, settings);
		}
	}

	public class CustomResolver : DefaultContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var prop = base.CreateProperty(member, memberSerialization);
			var propInfo = member as PropertyInfo;
			if (propInfo == null)
				return prop;

			if (propInfo.GetMethod.IsVirtual && !propInfo.GetMethod.IsFinal)
			{
				prop.ShouldSerialize = obj => false;
			}
			return prop;
		}

	}
}
