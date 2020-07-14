using EShop.Shared.Requests;
using EShop.Shared.Response;
using System.Threading.Tasks;

namespace EShop.FrontOffice.Services
{
    public interface IOrderService
    {
        public Task<OrderCreateResponse> SaveOrder(OrderCreateRequest order);
    }
}
