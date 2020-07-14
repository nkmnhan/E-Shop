using EShop.Shared.ViewModels.Cart;
using EShop.Shared.ViewModels.Product;
using Microsoft.AspNetCore.ProtectedBrowserStorage;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EShop.FrontOffice.Services
{
    public class CartService : ICartService
    {
        private List<CartLineVm> _lineCollection = new List<CartLineVm>();

        [JsonIgnore]
        private ProtectedSessionStorage _session { get; set; }

        public CartService(ProtectedSessionStorage session)
        {
            _session = session;
        }

        public async Task Reload()
        {
            _lineCollection = await _session.GetAsync<List<CartLineVm>>("Cart") ?? new List<CartLineVm>();
        }

        public async Task AddItem(ProductVm product, int quantity)
        {
            var line = _lineCollection
            .Where(p => p.Product.Id == product.Id)
            .FirstOrDefault();
            if (line == null)
            {
                _lineCollection.Add(new CartLineVm
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }

            await _session.SetAsync("Cart", _lineCollection);
        }

        public async Task RemoveAll()
        {
            _lineCollection.Clear();
            await _session.DeleteAsync("Cart");
        }

        public async Task RemoveItem(int productId)
        {
            _lineCollection.RemoveAll(l => l.Product.Id == productId);
            await _session.SetAsync("Cart", _lineCollection);
        }

        public IEnumerable<CartLineVm> Lines()
            => _lineCollection;

        public decimal ComputeTotalValue()
            => _lineCollection.Sum(e => e.Product.Price * e.Quantity);

        public int TotalItem()
       => _lineCollection.Sum(e => e.Quantity);

        public async Task SetRangeItem(ProductVm product, int quantity)
        {
            quantity = quantity < 0 ? 0 : quantity;

            var line = _lineCollection
             .Where(p => p.Product.Id == product.Id)
             .FirstOrDefault();
            if (line == null)
            {
                _lineCollection.Add(new CartLineVm
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity = quantity;
            }

            await _session.SetAsync("Cart", _lineCollection);
        }

        public async Task ReduceItem(int productId, int quantity)
        {
            quantity = quantity < 0 ? 0 : quantity;

            var line = _lineCollection
             .Where(p => p.Product.Id == productId)
             .FirstOrDefault();
            if (line != null)
            {
                line.Quantity = line.Quantity - quantity < 0 ? 0 : line.Quantity - quantity;
                await _session.SetAsync("Cart", _lineCollection);
            }

        }
    }
}
