using OpenTelemetryExample.Core.Models;
using System.Net.Http.Json;

namespace OpenTelemetryExample.Core.Clients
{
    public class Api1Client : IApi1Client
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public Api1Client(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> PostProductAsync(ProductDto productDto)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Post,
                "https://localhost:7185/Product")
            {
                Content = JsonContent.Create(productDto)
            };

            var httpClient = _httpClientFactory.CreateClient();
            var result = await httpClient.SendAsync(httpRequestMessage);
            return result.IsSuccessStatusCode;
        }
    }
}
