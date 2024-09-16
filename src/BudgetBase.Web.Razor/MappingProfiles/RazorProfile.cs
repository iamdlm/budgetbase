using AutoMapper;
using BudgetBase.Core.Application.DTOs.Identity;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Accounts;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Categories;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Imports;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Profile;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Rules;
using BudgetBase.Web.Razor.Areas.App.ViewModels.RulesGroup;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Transactions;

namespace BudgetBase.Web.Razor.MappingProfiles
{
    public class RazorProfile : Profile
    {
        public RazorProfile()
        {
            CreateMap<AccountViewModel, AccountDto>()
                .ReverseMap();

            CreateMap<CategoryViewModel, CategoryDto>()
               .ReverseMap();

            CreateMap<ProfileViewModel, ApplicationUserDto>()
                .ReverseMap();

            CreateMap<TransactionViewModel, TransactionDto>()
                .ReverseMap();

            CreateMap<ImportViewModel, ImportDto>()
                .ReverseMap();

            CreateMap<RuleViewModel, TransactionRuleDto>()
                .ReverseMap();
          
            CreateMap<RulesGroupViewModel, TransactionRulesGroupDto>()
                .ReverseMap();
        }
    }
}
