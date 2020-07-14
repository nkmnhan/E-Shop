using EShop.Shared.ViewModels.Cart;
using System.Collections.Generic;

namespace EShop.Shared.ViewModels.Order
{
    public class OrderVm
    {
        public int OrderID { get; set; }
        public IList<CartLineVm> Lines { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
