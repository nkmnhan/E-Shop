using EShop.Shared.ViewModels.Category;
using System.Collections.Generic;

namespace EShop.Shared.ViewModels.Product
{
    public class ProductVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public IList<CategoryVm> Categories { get; set; }
    }
}
