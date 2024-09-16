using BudgetBase.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Infrastructure.Persistence.Data;
using BudgetBase.Infrastructure.Persistence.Interceptors;
using BudgetBase.Core.Domain.Entities;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Services.Persistence;

namespace BudgetBase.Infrastructure.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppDbContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    Environment.GetEnvironmentVariable("BUDGETBASE_CONNECTION_STRING"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        }

        public static void AddInfrastructurePersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionTypeService, TransactionTypeService>();
            services.AddScoped<ITransactionEntryTypeService, TransactionEntryTypeService>();
            services.AddScoped<IRecurrencyTypeService, RecurrencyTypeService>();
            services.AddScoped<IImportService, ImportService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<ITransactionRuleService, TransactionRuleService>();
            services.AddScoped<ITransactionRuleFieldService, TransactionRuleFieldService>();
            services.AddScoped<ITransactionRuleConditionService, TransactionRuleConditionService>();
            services.AddScoped<ITransactionRulesGroupService, TransactionRulesGroupService>();
            services.AddScoped<ITransactionRulesGroupOperatorService, TransactionRulesGroupOperatorService>();
        }

        public static async Task AddTransactionTypesAsync(this IServiceProvider service)
        {
            var baseService = service.GetRequiredService<ITransactionTypeService>();

            foreach (TransactionType type in Enums.Types)
            {
                EnumDto existingType = null;
                try
                {
                    existingType = await baseService.GetByIdAsync(type.Id).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    // do nothing
                }

                if (existingType == null)
                {
                    await baseService.CreateAsync(new EnumDto
                    {
                        Id = type.Id,
                        Index = type.Index,
                        Description = type.Description
                    }).ConfigureAwait(false);
                }
            }
        }

        public static async Task AddTransactionEntryTypesAsync(this IServiceProvider service)
        {
            var baseService = service.GetRequiredService<ITransactionEntryTypeService>();

            foreach (TransactionEntryType type in Enums.EntryTypes)
            {
                EnumDto existingType = null;
                try
                {
                    existingType = await baseService.GetByIdAsync(type.Id).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    // do nothing
                }

                if (existingType == null)
                {
                    await baseService.CreateAsync(new EnumDto
                    {
                        Id = type.Id,
                        Index = type.Index,
                        Description = type.Description
                    }).ConfigureAwait(false);
                }
            }
        }

        public static async Task AddRecurrencyTypesAsync(this IServiceProvider service)
        {
            var baseService = service.GetRequiredService<IRecurrencyTypeService>();

            foreach (RecurrencyType type in Enums.RecurrencyTypes)
            {
                EnumDto existingType = null;
                try
                {
                    existingType = await baseService.GetByIdAsync(type.Id).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    // do nothing
                }

                if (existingType == null)
                {
                    await baseService.CreateAsync(new EnumDto
                    {
                        Id = type.Id,
                        Index = type.Index,
                        Description = type.Description
                    }).ConfigureAwait(false);
                }
            }
        }

        public static async Task AddCountriesAsync(this IServiceProvider service)
        {
            var unitOfWork = service.GetRequiredService<IUnitOfWork>();

            foreach (Country type in SeedData.Countries)
            {
                Country existingType = null;
                try
                {
                    existingType = await unitOfWork.CountryRepo.GetByIdAsync(type.Id).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    // do nothing
                }

                if (existingType == null)
                {
                    unitOfWork.CountryRepo.Add(new Country
                    {
                        Id = type.Id,
                        Name = type.Name,
                        Code = type.Code
                    });
                }
            }

            await unitOfWork.CompleteAsync().ConfigureAwait(false);
        }

        public static async Task AddBanksAsync(this IServiceProvider service)
        {
            var unitOfWork = service.GetRequiredService<IUnitOfWork>();

            foreach (Bank type in SeedData.Banks)
            {
                Bank existingType = null;
                try
                {
                    existingType = await unitOfWork.BankRepo.GetByIdAsync(type.Id).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    // do nothing
                }

                if (existingType == null)
                {
                    unitOfWork.BankRepo.Add(new Bank
                    {
                        Id = type.Id,
                        Name = type.Name,
                        CountryId = type.CountryId
                    });
                }
            }

            await unitOfWork.CompleteAsync().ConfigureAwait(false);
        }

        public static async Task AddTransactionRuleFieldsAsync(this IServiceProvider service)
        {
            var baseService = service.GetRequiredService<ITransactionRuleFieldService>();

            foreach (TransactionRuleField type in Enums.TransactionRuleFields)
            {
                EnumDto existingType = null;
                try
                {
                    existingType = await baseService.GetByIdAsync(type.Id).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    // do nothing
                }

                if (existingType == null)
                {
                    await baseService.CreateAsync(new EnumDto
                    {
                        Id = type.Id,
                        Index = type.Index,
                        Description = type.Description
                    }).ConfigureAwait(false);
                }
            }
        }

        public static async Task AddTransactionRuleConditionsAsync(this IServiceProvider service)
        {
            var baseService = service.GetRequiredService<ITransactionRuleConditionService>();

            foreach (TransactionRuleCondition type in Enums.TransactionRuleConditions)
            {
                EnumDto existingType = null;
                try
                {
                    existingType = await baseService.GetByIdAsync(type.Id).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    // do nothing
                }

                if (existingType == null)
                {
                    await baseService.CreateAsync(new EnumDto
                    {
                        Id = type.Id,
                        Index = type.Index,
                        Description = type.Description
                    }).ConfigureAwait(false);
                }
            }
        }

        public static async Task AddTransactionRulesGroupOperatorAsync(this IServiceProvider service)
        {
            var baseService = service.GetRequiredService<ITransactionRulesGroupOperatorService>();

            foreach (TransactionRulesGroupOperator type in Enums.TransactionRulesGroupOperators)
            {
                EnumDto existingType = null;
                try
                {
                    existingType = await baseService.GetByIdAsync(type.Id).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    // do nothing
                }

                if (existingType == null)
                {
                    await baseService.CreateAsync(new EnumDto
                    {
                        Id = type.Id,
                        Index = type.Index,
                        Description = type.Description
                    }).ConfigureAwait(false);
                }
            }
        }
    }
}
