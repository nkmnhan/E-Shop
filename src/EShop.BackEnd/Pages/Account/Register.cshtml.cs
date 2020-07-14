using EShop.BackEnd.Commons;
using EShop.BackEnd.Models;
using EShop.Shared.ViewModels.Identity;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EShop.BackEnd.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }

        [BindProperty]
        public RegisterVm Input { get; set; }

        public string RedirectUrl { get; set; }

        public string ClientName { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task OnGetAsync(string redirectUrl = null, string clientName = null)
        {
            RedirectUrl = redirectUrl;
            ClientName = clientName;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string redirectUrl = null, string clientName = null)
        {
            redirectUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new User { UserName = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, $"Email {Input.Email} is already in use.");
                    return Page();
                }

                result = await _userManager.AddClaimsAsync(user,
                    new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, $"{Input.FirstName} {Input.LastName}"),
                        new Claim(JwtClaimTypes.GivenName, Input.FirstName),
                        new Claim(JwtClaimTypes.FamilyName, Input.LastName),
                        new Claim(JwtClaimTypes.Email, Input.Email),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    });

                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(user, Constants.UserRole);
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    if (string.IsNullOrEmpty(clientName))
                    {
                        return LocalRedirect("~/");
                    }
                    else
                    {
                        var clientUrl = _configuration[$"ClientUrl:{clientName}"];
                        return Redirect($"{clientUrl}/authentication/login?redirectUrl={redirectUrl}");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
