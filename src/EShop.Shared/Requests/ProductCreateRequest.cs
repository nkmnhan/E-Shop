using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EShop.Shared.Requests
{
    public class ProductCreateRequest
    {
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int BrandId { get; set; }

        public IList<int> CategoryIds { get; set; }
    }
}
