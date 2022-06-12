namespace OpenTelemetryExample.Core.Models
{
    public class KafkaOptions
    {
        public string Servers { get; set; }
        public Producer Producer { get; set; }
    }

    public class Producer
    {
        public int? SocketTimeoutMs { get; set; }
        public int? MessageTimeoutMs { get; set; }
    }
}
