using EShop.Shared.ViewModels.Product;
using System;
using System.Threading.Tasks;

namespace EShop.FrontOffice.Services
{
    public interface INotifierCartService
    {
        public event Func<Task> Notify;
        public Task AddCartLine(ProductVm product, int quantity);
        public Task SetRangeCartLine(ProductVm product, int quantity);
        public Task ReduceCartLine(int productId, int quantity);
        public Task RemoveCartLine(int productId);
        public Task RemoveAllCartLines();
    }
}
