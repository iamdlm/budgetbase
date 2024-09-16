using BudgetBase.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BudgetBase.Core.Application.Interfaces.Identity;
using BudgetBase.Infrastructure.Identity.Services;
using AutoMapper;
using BudgetBase.Infrastructure.Identity.MappingProfiles;
using BudgetBase.Infrastructure.Identity.Data;
using System.Data;
using System;

namespace BudgetBase.Infrastructure.Identity.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIdentityDbContext(this IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseNpgsql(
                    Environment.GetEnvironmentVariable("BUDGETBASE_CONNECTION_STRING"),
                    b => b.MigrationsAssembly(typeof(AppIdentityDbContext).Assembly.FullName)));
        }

        public static void AddIdentityAuth(this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddInfrastructureIdentityServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddInfrastructureIdentityMappingProfile(this IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile(new InfrastructureIdentityProfile());
        }

        public static async Task AddUserRolesAsync(this IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (IdentityRole role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name).ConfigureAwait(false))
                {
                    await roleManager.CreateAsync(role).ConfigureAwait(false);
                }
            }
        }

        private static readonly IdentityRole[] Roles =
        {
            new IdentityRole { Id = "1ae76ade-92b5-4744-1bc1-d2832fe908b1", Name = Constants.FreeRole, NormalizedName = Constants.FreeRole.ToUpperInvariant() },
            new IdentityRole { Id = "2ae76ade-92b5-4744-2bc2-d2832fe908b2", Name = Constants.ProRole, NormalizedName = Constants.ProRole.ToUpperInvariant() },
            new IdentityRole { Id = "3ae76ade-92b5-4744-3bc3-d2832fe908b3", Name = Constants.LifetimeRole, NormalizedName = Constants.LifetimeRole.ToUpperInvariant() }
        };
    }
}
