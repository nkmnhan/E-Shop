using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;

namespace EShop.FrontOffice.Comons
{
    public static class PageUrls
    {
        public static string LoginPage(string redirectUrl) => QueryHelpers.AddQueryString(@"authentication/login", new Dictionary<string, string>() { { "redirectUrl", redirectUrl } });
        public static string RegisterPage(string backEndUrl, string redirectUrl)
            => QueryHelpers.AddQueryString(@$"{backEndUrl}/Account/register",
                new Dictionary<string, string>()
                {
                    { "redirectUrl", redirectUrl },
                    { "clientName", "FrontOffice" }
                });
        public static string ProductsPage(int categoryId, int pageIndex, string SearchContent = "") => $"/products/{categoryId}/{pageIndex}/{SearchContent}";
    }
}
