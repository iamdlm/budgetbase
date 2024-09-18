using BudgetBase.Infrastructure.Persistence.Extensions;
using BudgetBase.Infrastructure.Identity.Extensions;
using BudgetBase.Web.Razor.Extensions;
using BudgetBase.Infrastructure.Common.Extensions;
using BudgetBase.Core.Application.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

namespace BudgetBase.Web.Razor
{
    public static class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "ASYNC0001:Asynchronous method names should end with Async", Justification = "Program does not contain a static 'Main' method suitable for an entry point.")]
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureOptions();

            // Add Db contexts
            builder.Services.AddAppDbContext();
            builder.Services.AddIdentityDbContext();

            // Add identity
            builder.Services.AddIdentityAuth();

            // Add services
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureCommonServices();
            builder.Services.AddInfrastructureIdentityServices();
            builder.Services.AddInfrastructurePersistenceServices();
            builder.Services.AddWebRazorServices();

            builder.Services.AddHttpContextAccessor();

            // Add mapping profiles
            builder.Services.AddMapperConfiguration();

            // Add claims
            builder.Services.AddAuthorizationClaims();

            // Add required authorization Areas
            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeAreaFolder("App", "/");
            });

            // Configura Forwarded Headers middleware
            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            // Configure cookie
            builder.Services.ConfigureCookie();

            // Configure lowercase URLs
            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseForwardedHeaders();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                await serviceProvider.AddUserRolesAsync().ConfigureAwait(false);
                await serviceProvider.AddTransactionTypesAsync().ConfigureAwait(false);
                await serviceProvider.AddTransactionEntryTypesAsync().ConfigureAwait(false);
                await serviceProvider.AddRecurrencyTypesAsync().ConfigureAwait(false);
                await serviceProvider.AddCountriesAsync().ConfigureAwait(false);
                await serviceProvider.AddBanksAsync().ConfigureAwait(false);
                await serviceProvider.AddTransactionRuleFieldsAsync().ConfigureAwait(false);
                await serviceProvider.AddTransactionRuleConditionsAsync().ConfigureAwait(false);
                await serviceProvider.AddTransactionRulesGroupOperatorAsync().ConfigureAwait(false);
            }

            app.MapRazorPages();

            app.Run();
        }
    }
}
