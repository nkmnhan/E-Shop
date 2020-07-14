using System.Collections.Generic;

namespace EShop.BackEnd.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<ProductCategory> ProductCategories { get; private set; } = new List<ProductCategory>();
    }
}
