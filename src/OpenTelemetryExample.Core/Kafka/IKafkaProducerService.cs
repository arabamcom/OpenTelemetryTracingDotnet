using OpenTelemetryExample.Core.Kafka.Events;
using OpenTelemetryExample.Core.Models;

namespace OpenTelemetryExample.Core.Kafka
{
    public interface IKafkaProducerService : IDisposable
    {
        IOperationResult Produce(IEvent eventSource, string topic = null);
        Task<IOperationResult> ProduceAsync(IEvent eventSource, string topic = null);
    }
}
