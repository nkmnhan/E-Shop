using System.ComponentModel.DataAnnotations;

namespace EShop.Shared.Requests
{
    public class BrandUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
