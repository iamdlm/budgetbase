using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Domain.Entities;

namespace BudgetBase.Core.Application.MappingProfiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Account, AccountDto>()
                .ReverseMap();

            CreateMap<TransactionCategory, CategoryDto>()
               .ReverseMap();

            CreateMap<Transaction,  TransactionDto>()
                .ReverseMap();

            CreateMap<TransactionType, EnumDto>()
                .ReverseMap();

            CreateMap<TransactionEntryType, EnumDto>()
                .ReverseMap();

            CreateMap<RecurrencyType, EnumDto>()
                .ReverseMap();

            CreateMap<Country, CountryDto>()
                .ReverseMap();

            CreateMap<Bank, BankDto>()
                .ReverseMap();

            CreateMap<Import, ImportDto>()
                .ReverseMap();

            CreateMap<TransactionRuleField, EnumDto>()
                .ReverseMap();

            CreateMap<TransactionRuleCondition, EnumDto>()
                .ReverseMap();

            CreateMap<TransactionRule, TransactionRuleDto>()
               .ReverseMap();

            CreateMap<TransactionRulesGroup, TransactionRulesGroupDto>()
               .ReverseMap();

            CreateMap<TransactionRulesGroupOperator, EnumDto>()
               .ReverseMap();
        }
    }
}
