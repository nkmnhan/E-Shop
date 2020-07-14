using System.ComponentModel.DataAnnotations;

namespace EShop.Shared.Requests
{
    public class CategoryCreateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
