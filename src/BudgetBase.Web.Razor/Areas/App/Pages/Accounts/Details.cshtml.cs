using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Accounts;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Accounts
{
    public class AccountDetailsModel : BreadcrumbPageModel
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public AccountDetailsModel(IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        public AccountViewModel Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                AccountDto dto = await _accountService.GetByIdAsync(id.Value).ConfigureAwait(false);

                if (dto == null)
                {
                    return NotFound();
                }
                else
                {
                    Account = _mapper.Map<AccountViewModel>(dto);
                }
            }
            catch (ValidationException ex)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
