using BudgetBase.Core.Application.DTOs.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Helpers
{
    public static class SelectListHelper
    {
        public static void BuildCategorySelectList(IEnumerable<CategoryDto> categories, Guid? parentId, List<SelectListItem> selectListItems, string prefix = "")
        {
            foreach (var category in categories.Where(c => c.ParentTransactionCategoryId == parentId))
            {
                selectListItems.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = prefix + " " + category.Title
                });

                // Recursive call for child categories
                BuildCategorySelectList(categories, category.Id, selectListItems, prefix + "-");
            }
        }
    }
}
