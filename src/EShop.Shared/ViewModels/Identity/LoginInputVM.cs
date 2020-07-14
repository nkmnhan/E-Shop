using System.ComponentModel.DataAnnotations;

namespace EShop.Shared.ViewModels.Identity
{
    public class LoginInputVm
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}