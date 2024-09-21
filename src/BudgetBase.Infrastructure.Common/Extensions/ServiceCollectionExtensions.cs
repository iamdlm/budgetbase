using BudgetBase.Core.Application.Interfaces.Application;
using BudgetBase.Core.Application.Interfaces.Common;
using BudgetBase.Core.Domain.Configurations;
using BudgetBase.Infrastructure.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using BudgetBase.Infrastructure.Common.Middlewares;

namespace BudgetBase.Infrastructure.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureCommonServices(this IServiceCollection services)
        {
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddScoped<IEmailService, EmailService>();
        }

        public static void ConfigureOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection(SmtpOptions.Smtp));
            builder.Services.Configure<EmailConfirmationOptions>(builder.Configuration.GetSection(EmailConfirmationOptions.EmailConfirmation));
        }

        public static void UseErrorLoggingMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ErrorLoggingMiddleware>();
        }
    }
}
