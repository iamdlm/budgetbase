using BudgetBase.Core.Application.MappingProfiles;
using AutoMapper;
using BudgetBase.Core.Application.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;
using BudgetBase.Core.Application.Parsers;
using BudgetBase.Core.Application.Services.Application;

namespace BudgetBase.Core.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationMappingProfile(this IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile(new ApplicationProfile());
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBankTransactionParserService, BankTransactionParserService>();
            services.AddScoped<MilleniumbcpTransactionParser>();
            services.AddScoped<ActivoBankTransactionParser>();
        }
    }
}
