using AutoMapper;
using BudgetBase.Core.Application.Extensions;
using BudgetBase.Core.Application.Interfaces.Application;
using BudgetBase.Infrastructure.Identity.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using BudgetBase.Web.Razor.MappingProfiles;
using BudgetBase.Web.Razor.Services;

namespace BudgetBase.Web.Razor.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMapperConfiguration(this IServiceCollection services)
        {
            MapperConfiguration mapperConfig = new(mc =>
            {
                mc.AddApplicationMappingProfile();
                mc.AddInfrastructureIdentityMappingProfile();
                mc.AddRazorMappingProfile();
            });

            services.AddSingleton(mapperConfig.CreateMapper());
        }

        public static void AddAuthorizationClaims(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("EmailConfirmed", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("emailConfirmed", "True");
                });
            });
        }

        public static void AddWebRazorServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }

        public static void AddRazorMappingProfile(this IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile(new RazorProfile());
        }

        public static void ConfigureCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/auth/signin";
                options.LogoutPath = "/auth/signout";
                options.AccessDeniedPath = "/auth/confirmation";
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToAccessDenied = context =>
                    {
                        string email = context.HttpContext.User?.FindFirstValue(ClaimTypes.Email);
                        string url = $"{context.RedirectUri}&email={email}";
                        context.Response.Redirect(url);
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
