using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EShop.BackEnd.Data.SeedData
{
    [ExcludeFromCodeCoverage]
    public static class IdmConfigData
    {
        public static IEnumerable<IdentityResource> Ids =>
               new List<IdentityResource>
               {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
               };


        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("eshop-api", "EShop-Api")
                {
                    ApiSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "front-office",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,

                    AllowedCorsOrigins = { "https://localhost:5002",
                                           "https://localhost:5003" },
                
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc",
                                     "https://localhost:5003/signin-oidc"},

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc",
                                               "https://localhost:5003/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "eshop-api"
                    },

                    AllowOfflineAccess = true
                },
                new Client
                {
                    ClientId = "swagger",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,

                    AllowedCorsOrigins = { "http://localhost:5000",
                                           "https://localhost:5001" },
                
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5000/swagger/oauth2-redirect.html",
                                     "https://localhost:5001/swagger/oauth2-redirect.html"},

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5000/swagger/oauth2-redirect.html",
                                               "https://localhost:5001/swagger/oauth2-redirect.html"},

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "eshop-api"
                    }
                },
                new Client
                {
                    ClientId = "back-office",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,

                    AllowedCorsOrigins = { "http://localhost:3000",
                    "https://localhost:9090"},
                
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:3000/callback",
                    "https://localhost:9090/callback"},

                    //// where to redirect to after logout
                    //PostLogoutRedirectUris = { "http://localhost:3000/swagger/oauth2-redirect.html",
                    //                           "https://localhost:5001/swagger/oauth2-redirect.html"},

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "eshop-api"
                    },
                    AlwaysIncludeUserClaimsInIdToken = true
                }
            };
    }
}
