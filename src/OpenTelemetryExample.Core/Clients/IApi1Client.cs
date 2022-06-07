using OpenTelemetryExample.Core.Models;

namespace OpenTelemetryExample.Core.Clients
{
    public interface IApi1Client
    {
        Task<bool> PostProductAsync(ProductDto productDto);
    }
}
