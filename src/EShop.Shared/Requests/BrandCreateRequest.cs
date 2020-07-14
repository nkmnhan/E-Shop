using System.ComponentModel.DataAnnotations;

namespace EShop.Shared.Requests
{
    public class BrandCreateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
