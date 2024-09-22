using AutoMapper;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Web.Razor.Areas.App.Pages.Helpers;
using BudgetBase.Core.Application.Interfaces.Identity;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Transactions
{
    public abstract class TransactionPageModelBase : BreadcrumbPageModel
    {
        protected readonly IAccountService _accountService;
        protected readonly ICategoryService _categoryService;
        private readonly ITransactionEntryTypeService _entryTypesService;
        private readonly IRecurrencyTypeService _recurrenciesService;

        protected TransactionPageModelBase(
            IAccountService accountService,
            ICategoryService categoryService,
            ITransactionEntryTypeService entryTypesService,
            IRecurrencyTypeService recurrenciesService,
            IUserService userService) : base(userService)
        {
            _accountService = accountService;
            _categoryService = categoryService;
            _entryTypesService = entryTypesService;
            _recurrenciesService = recurrenciesService;
        }

        protected async Task SetAccountIdSelectListAsync(Guid? selectedSourceAccountId, Guid? selectedTargetAccount)
        {
            var accounts = await _accountService.GetAllAsync().ConfigureAwait(false);
            ViewData["SourceAccountId"] = new SelectList(accounts, "Id", "Name", selectedSourceAccountId);
            ViewData["TargetAccountId"] = new SelectList(accounts, "Id", "Name", selectedTargetAccount);
        }

        protected async Task SetTransactionCategoryIdSelectListAsync(object selectedTransactionCategoryId = null)
        {
            var categories = await _categoryService.GetAllAsync().ConfigureAwait(false);
            var selectListItems = new List<SelectListItem>();
            SelectListHelper.BuildCategorySelectList(categories.OrderBy(c => c.Title), null, selectListItems);

            ViewData["TransactionCategoryId"] = new SelectList(selectListItems, "Value", "Text", selectedTransactionCategoryId);
        }

        protected async Task SetTransactionEntryTypeSelectListAsync(object selectedEntryTypeId = null)
        {
            var entryTypes = await _entryTypesService.GetAllAsync().ConfigureAwait(false);
            ViewData["TransactionEntryTypeId"] = new SelectList(entryTypes, "Id", "Description", selectedEntryTypeId);
        }

        protected async Task SetRecurrencyTypeSelectListAsync(object selectedRecurrencyTypeId = null)
        {
            var entryTypes = await _recurrenciesService.GetAllAsync().ConfigureAwait(false);
            ViewData["RecurrencyTypeId"] = new SelectList(entryTypes, "Id", "Description", selectedRecurrencyTypeId);
        }
    }
}
