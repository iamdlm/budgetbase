using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Core.Application.DTOs.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.Pages.Helpers;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Categories
{
    public abstract class CategoryPageModelBase : BreadcrumbPageModel
    {
        protected readonly ICategoryService _categoryService;
        private readonly ITransactionTypeService _enumService;

        protected CategoryPageModelBase(ICategoryService categoryService, ITransactionTypeService enumService)
        {
            _categoryService = categoryService;
            _enumService = enumService;
        }

        protected async Task SetParentCategoryIdSelectListAsync(object selectedCategoryId = null)
        {
            var categories = await _categoryService.GetAllAsync().ConfigureAwait(false);
            var selectListItems = new List<SelectListItem>();
            SelectListHelper.BuildCategorySelectList(categories.OrderBy(c => c.Title), null, selectListItems);

            ViewData["CategoryId"] = new SelectList(selectListItems, "Value", "Text", selectedCategoryId);
        }

        protected async Task SetTransactionTypeSelectListAsync(object selectedTypeId = null)
        {
            var categories = await _enumService.GetAllAsync().ConfigureAwait(false);
            ViewData["TransactionTypeId"] = new SelectList(categories, "Id", "Description", selectedTypeId);
        }

        public Task<JsonResult> OnPostFillCategoriesSelectListAsync(Guid typeId)
        {
            var categories = _categoryService.GetByType(typeId);

            var selectListItems = new List<SelectListItem>();
            SelectListHelper.BuildCategorySelectList(categories.OrderBy(c => c.Title), null, selectListItems);

            return Task.FromResult(new JsonResult(new SelectList(selectListItems, "Value", "Text")));
        }
    }
}
