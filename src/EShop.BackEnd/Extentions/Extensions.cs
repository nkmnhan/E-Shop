using EShop.Shared.ViewModels.Identity;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EShop.BackEnd.Extentions
{
    public static class Extensions
    {
        public static async Task<bool> IsPkceClientAsync(this IClientStore store, string client_id)
        {
            if (!string.IsNullOrWhiteSpace(client_id))
            {
                var client = await store.FindEnabledClientByIdAsync(client_id);
                return client?.RequirePkce == true;
            }

            return false;
        }

        public static IActionResult LoadingPage(this PageModel pageModel, string viewName, string redirectUri)
        {
            pageModel.HttpContext.Response.StatusCode = 200;
            pageModel.HttpContext.Response.Headers["Location"] = "";

            return pageModel.RedirectToPage(viewName, new RedirectVm { RedirectUrl = redirectUri });
        }
    }
}
