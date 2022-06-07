using OpenTelemetryExample.Core.Models;

namespace OpenTelemetryExample.Core.Services
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int id);
        Task InsertProductAsync(ProductDto productDto);
    }
}
