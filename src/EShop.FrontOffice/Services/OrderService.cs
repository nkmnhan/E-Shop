using EShop.FrontOffice.Comons;
using EShop.Shared.Requests;
using EShop.Shared.Response;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EShop.FrontOffice.Services
{
    public class OrderService : IOrderService
    {
        private readonly IApiServive _apiServive;

        public OrderService(IApiServive apiServive)
        {
            _apiServive = apiServive;
        }

        public async Task<OrderCreateResponse> SaveOrder(OrderCreateRequest order)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, BackEndUrls.SaveOrder())
            {
                Content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json")
            };

            return await _apiServive.SendAsync<OrderCreateResponse>(request);
        }
    }
}
