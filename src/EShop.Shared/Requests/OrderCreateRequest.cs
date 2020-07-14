using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EShop.Shared.Requests
{
    public class OrderCreateRequest
    {
        public IList<CartLineCreateRequest> Lines { get; set; }
        [Required(ErrorMessage = "The phone number field is required.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "The address field is required.")]
        public string Address { get; set; }
    }
}
