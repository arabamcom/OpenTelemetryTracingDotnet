namespace OpenTelemetryExample.Core.Models
{
    public class KafkaOptions
    {
        public string Servers { get; set; }
        public Credentials Credentials { get; set; }
        public Producer Producer { get; set; }
    }

    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Producer
    {
        public int? SocketTimeoutMs { get; set; }
        public int? MessageTimeoutMs { get; set; }
    }
}
