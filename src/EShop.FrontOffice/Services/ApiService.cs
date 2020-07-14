using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace EShop.FrontOffice.Services
{
    public class ApiService : IApiServive
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClientFactory)
        {
            _httpClient = httpClientFactory;
        }

        public async Task<TResult> SendAsync<TResult>(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResult>(content);
        }
    }
}
