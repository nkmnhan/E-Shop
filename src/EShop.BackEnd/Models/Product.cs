using System.Collections.Generic;

namespace EShop.BackEnd.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public Brand Brand { get; set; }

        public IList<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
