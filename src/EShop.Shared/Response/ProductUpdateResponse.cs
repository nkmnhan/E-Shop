using System.Collections.Generic;

namespace EShop.Shared.Response
{
    public class ProductUpdateResponse : BaseResponse
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int BrandId { get; set; }

        public IList<int> CategoryIds { get; set; }
    }
}
