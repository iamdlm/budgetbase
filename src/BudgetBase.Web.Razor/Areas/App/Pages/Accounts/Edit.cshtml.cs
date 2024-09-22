using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Accounts;
using BudgetBase.Web.Razor.Helpers;
using BudgetBase.Core.Application.Interfaces.Identity;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Accounts
{
    public class AccountEditModel : BreadcrumbPageModel
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public AccountEditModel(IMapper mapper, IAccountService accountService, IUserService userService) : base(userService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        [BindProperty]
        public AccountViewModel Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AccountDto dto = await _accountService.GetByIdAsync(id.Value).ConfigureAwait(false);

            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                Account = _mapper.Map<AccountViewModel>(dto);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                AccountDto dto = _mapper.Map<AccountDto>(Account);
                await _accountService.UpdateAsync(dto).ConfigureAwait(false);
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
