using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EShop.FrontOffice.Pages
{
    public class AuthenticationModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnGetLogin(string redirectUrl)
        {
            var authProps = new AuthenticationProperties
            {
                RedirectUri = Url.Content(redirectUrl)
            };
            return Challenge(authProps, "oidc");
        }

        public async Task OnGetLogout()
        {
            var authProps = new AuthenticationProperties
            {
                RedirectUri = Url.Content("~/")
            };
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc", authProps);
        }
    }
}