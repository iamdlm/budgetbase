using BudgetBase.Core.Application.Interfaces.Identity;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Web.Razor.Areas.App.Pages.Helpers;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetBase.Web.Razor.Areas.App.Pages.RulesGroups
{
    public abstract class RulesGroupPageModelBase : BreadcrumbPageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly ITransactionRulesGroupOperatorService _operatorService;

        protected RulesGroupPageModelBase(
            ICategoryService categoryService,
            ITransactionRulesGroupOperatorService operatorService,
            IUserService userService) : base(userService)
        {
            _categoryService = categoryService;
            _operatorService = operatorService;
        }

        protected async Task SetCategoriesSelectListAsync(object selectedTransactionCategoryId = null)
        {
            var categories = await _categoryService.GetAllAsync().ConfigureAwait(false);
            var selectListItems = new List<SelectListItem>();
            SelectListHelper.BuildCategorySelectList(categories.OrderBy(c => c.Title), null, selectListItems);

            ViewData["TransactionCategoryId"] = new SelectList(selectListItems, "Value", "Text", selectedTransactionCategoryId);
        }

        protected async Task SetRulesGroupOperatorSelectListAsync(object selectedTransactionRulesGroupOperatorId = null)
        {
            var operators = await _operatorService.GetAllAsync().ConfigureAwait(false);
            ViewData["TransactionRulesGroupOperatorId"] = new SelectList(operators, "Id", "Description", selectedTransactionRulesGroupOperatorId);
        }

        protected async Task SetSelectListsAsync()
        {
            await SetCategoriesSelectListAsync().ConfigureAwait(false);
            await SetRulesGroupOperatorSelectListAsync().ConfigureAwait(false);
        }
    }
}
