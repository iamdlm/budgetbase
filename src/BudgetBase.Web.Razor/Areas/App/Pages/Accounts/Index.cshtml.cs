using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Interfaces.Identity;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Accounts;
using System.Linq.Expressions;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Accounts
{
    public class AccountIndexModel : BaseIndexModel<AccountViewModel, AccountViewModel, AccountDto>
    {
        public AccountIndexModel(IMapper mapper, IAccountService accountService, IUserService userService)
             : base(mapper, accountService, userService)
        {
        }

        public IList<AccountViewModel> Accounts { get; set; } = default!;
        
        protected override Expression<Func<AccountViewModel, object>> GetSortPropertyExpression(string sortField)
        {
            switch (sortField)
            {
                case "Name":
                    return m => m.Name;
                case "Number":
                    return m => m.Number;
                case "IBAN":
                    return m => m.IBAN;
                default:
                    return m => m.Name;
            }
        }

        protected override Expression<Func<AccountViewModel, object>> GetIncludePropertiesExpression()
        {
            return m => new
            {
                m.Name,
                m.Number,
                m.IBAN
            };
        }

        protected override List<AccountViewModel> FormatData(IEnumerable<AccountViewModel> data)
        {
            return data.Select(item => new AccountViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Number = item.Number,
                IBAN = item.IBAN,
                Actions = item.Id
            }).ToList();
        }
    }
}
