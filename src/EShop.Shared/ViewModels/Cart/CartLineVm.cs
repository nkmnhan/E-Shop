using EShop.Shared.ViewModels.Product;

namespace EShop.Shared.ViewModels.Cart
{
    public class CartLineVm
    {
        public int CartLineID { get; set; }
        public ProductVm Product { get; set; }
        public int Quantity { get; set; }
    }
}
