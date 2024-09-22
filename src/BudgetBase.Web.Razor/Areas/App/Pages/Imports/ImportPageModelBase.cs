using AutoMapper;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Core.Application.Services;
using BudgetBase.Core.Application.Interfaces.Identity;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Imports
{
    public abstract class ImportPageModelBase : BreadcrumbPageModel
    {
        protected readonly ICountryService _countryService;
        protected readonly IBankService _bankService;
        protected readonly IAccountService _accountService;

        protected ImportPageModelBase(ICountryService countryService, IBankService bankService, IAccountService accountService, IUserService userService) : base(userService)
        {
            _countryService = countryService;
            _bankService = bankService;
            _accountService = accountService;
        }

        protected async Task SetCountryIdSelectListAsync(object selectedCountryId = null)
        {
            var countries = await _countryService.GetAllWithAssociatedBankAsync().ConfigureAwait(false);
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", selectedCountryId);
        }

        protected Task SetBankIdSelectListAsync(object selectedCountryId = null, object selectedBankId = null)
        {
            var banks = _bankService.GetByCountryId((Guid)selectedCountryId);
            ViewData["BankId"] = new SelectList(banks, "Id", "Name", selectedBankId);
            return Task.CompletedTask;
        }

        protected async Task SetAccountIdSelectListAsync(object selectedSourceAccountId = null)
        {
            var accounts = await _accountService.GetAllAsync().ConfigureAwait(false);
            ViewData["SourceAccountId"] = new SelectList(accounts, "Id", "Name", selectedSourceAccountId);
        }
    }
}
