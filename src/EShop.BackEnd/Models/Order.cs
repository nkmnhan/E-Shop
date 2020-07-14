using System.Collections.Generic;

namespace EShop.BackEnd.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public IList<CartLine> Lines { get; set; }
        public User User { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
