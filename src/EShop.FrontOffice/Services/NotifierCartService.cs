using EShop.Shared.ViewModels.Product;
using System;
using System.Threading.Tasks;

namespace EShop.FrontOffice.Services
{
    public class NotifierCartService : INotifierCartService
    {
        private readonly ICartService _cartService;

        public NotifierCartService(ICartService cartService)
        {
            _cartService = cartService;
        }

        public event Func<Task> Notify;

        public async Task AddCartLine(ProductVm product, int quantity)
        {
            await _cartService.AddItem(product, quantity);
            await NotifyChange();
        }

        public async Task SetRangeCartLine(ProductVm product, int quantity)
        {
            await _cartService.SetRangeItem(product, quantity);
            await NotifyChange();
        }

        public async Task ReduceCartLine(int productId, int quantity)
        {
            await _cartService.ReduceItem(productId, quantity);
            await NotifyChange();
        }

        public async Task RemoveCartLine(int productId)
        {
            await _cartService.RemoveItem(productId);
            await NotifyChange();
        }

        public async Task RemoveAllCartLines()
        {
            await _cartService.RemoveAll();
            await NotifyChange();
        }

        private async Task NotifyChange()
        {
            if (Notify != null)
            {
                await Notify?.Invoke();
            }
        }
    }
}
