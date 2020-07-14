using EShop.BackEnd.Models;
using EShop.Shared.Requests;
using EShop.Shared.Response;
using EShop.Shared.ViewModels.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.BackEnd.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderVm>> GetAllOrders();

        Task<OrderVm> GetOrdersById(int orderId);

        Task<IEnumerable<OrderVm>> GetOrdersByUserId(string userId);

        Task<OrderCreateResponse> SaveOrderAsync(OrderCreateRequest request, User user);
    }
}
