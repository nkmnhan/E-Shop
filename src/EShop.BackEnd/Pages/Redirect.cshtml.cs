using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.BackEnd.Pages.Account
{
    public class RedirectModel : PageModel
    {
        public string RedirectUrl { get; set; }
        public void OnGet(string redirectUrl)
        {
            RedirectUrl = redirectUrl;
        }
    }
}