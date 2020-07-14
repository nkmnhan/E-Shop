using EShop.BackEnd.Models;
using EShop.Shared.ViewModels.Identity;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EShop.BackEnd.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;

        public LogoutModel(SignInManager<User> signInManager,
            IIdentityServerInteractionService interaction,
            IEventService events)
        {
            _signInManager = signInManager;
            _interaction = interaction;
            _events = events;
        }

        public LogoutVm Input { get; set; }

        public async Task<IActionResult> OnGet(string logoutId)
        {
            Input = await BuildLogoutViewModelAsync(logoutId);

            if (Input.ShowLogoutPrompt == false)
            {
                return await OnPost();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var loggedOutVm = await BuildLoggedOutViewModelAsync(Input.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                await _signInManager.SignOutAsync();

                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            if (loggedOutVm.TriggerExternalSignout)
            {
                string url = Url.Action("Logout", new { logoutId = loggedOutVm.LogoutId });

                return SignOut(new AuthenticationProperties { RedirectUri = url }, loggedOutVm.ExternalAuthenticationScheme);
            }

            return RedirectToPage("./LoggedOut", loggedOutVm);
        }

        private async Task<LogoutVm> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutVm { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            return vm;
        }

        private async Task<LoggedOutVm> BuildLoggedOutViewModelAsync(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutVm
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }
    }
}