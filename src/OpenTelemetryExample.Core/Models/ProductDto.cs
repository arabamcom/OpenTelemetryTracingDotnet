using OpenTelemetryExample.Core.Kafka.Events;

namespace OpenTelemetryExample.Core.Models
{
    public class ProductDto : IEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public int Price { get; set; }

        public IEventTopicType Topic => IEventTopicType.AddWeatherForecastV1;
    }
}
