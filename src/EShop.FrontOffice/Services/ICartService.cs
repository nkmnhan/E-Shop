using EShop.Shared.ViewModels.Cart;
using EShop.Shared.ViewModels.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.FrontOffice.Services
{
    public interface ICartService
    {
        public IEnumerable<CartLineVm> Lines();
        public Task SetRangeItem(ProductVm product, int quantity);
        public Task AddItem(ProductVm product, int quantity);
        public Task ReduceItem(int productId, int quantity);
        public Task RemoveItem(int productId);
        public Task RemoveAll();
        public decimal ComputeTotalValue();
        public int TotalItem();
        public Task Reload();
    }
}
