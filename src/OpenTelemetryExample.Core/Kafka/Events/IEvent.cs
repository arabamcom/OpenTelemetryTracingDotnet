using System.Text.Json.Serialization;

namespace OpenTelemetryExample.Core.Kafka.Events
{
    public interface IEvent
    {
        [JsonIgnore]
        IEventTopicType Topic { get; }
    }
}
