using AutoMapper;
using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Accounts;
using BudgetBase.Web.Razor.Helpers;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Accounts
{
    public class AccountCreateModel : BreadcrumbPageModel
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public AccountCreateModel(IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AccountViewModel Account { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Account == null)
            {
                return Page();
            }

            try
            {
                AccountDto dto = _mapper.Map<AccountDto>(Account);
                await _accountService.CreateAsync(dto).ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
                ModelState.AddValidationErrorsToModelState(ex.Errors);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
