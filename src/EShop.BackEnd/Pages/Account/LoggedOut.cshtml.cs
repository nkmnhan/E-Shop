using EShop.Shared.ViewModels.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.BackEnd.Pages.Account
{
    public class LoggedOutModel : PageModel
    {
        public LoggedOutVm Input { get; set; }
        public void OnGet(LoggedOutVm loggedOutVm)
        {
            Input = loggedOutVm;
        }
    }
}