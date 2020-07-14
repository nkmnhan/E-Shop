using EShop.FrontOffice.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EShop.FrontOffice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
               .AddCookie("Cookies")
               .AddOpenIdConnect("oidc", options =>
               {
                   options.Authority = "http://localhost:5000";
                   options.RequireHttpsMetadata = false;

                   options.ClientId = "front-office";
                   options.ClientSecret = "secret";
                   options.ResponseType = "code";

                   options.GetClaimsFromUserInfoEndpoint = true;

                   options.SaveTokens = true;
                   options.Scope.Add("email");
                   options.Scope.Add("eshop-api");
                   options.Scope.Add("offline_access");

                   options.Events = new OpenIdConnectEvents
                   {
                       // called if user clicks Cancel during login
                       OnAccessDenied = context =>
                       {
                           context.HandleResponse();
                           context.Response.Redirect("/");
                           return Task.CompletedTask;
                       }
                   };
               });


            var configureClient = new Action<IServiceProvider, HttpClient>(async (provider, client) =>
            {
                var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
                var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");

                client.BaseAddress = new Uri(Configuration["BackEndUrl"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddProtectedBrowserStorage();
            services.AddHttpClient<IApiServive, ApiService>(configureClient);
            services.AddScoped<ICategoryService, CategorySerice>();
            services.AddScoped<IProductSerice, ProductSerice>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<INotifierCartService, NotifierCartService>();
            services.AddScoped<IOrderService, OrderService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
