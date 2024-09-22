using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Interfaces.Identity;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Categories;
using System.Linq.Expressions;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Categories
{
    public class CategoryIndexModel : BaseIndexModel<CategoryListViewModel, CategoryViewModel, CategoryDto>
    {
        public CategoryIndexModel(IMapper mapper, ICategoryService categoryService, IUserService userService)
            : base(mapper, categoryService, userService)
        {
        }

        public IList<CategoryViewModel> Categories { get; set; } = default!;

        protected override Expression<Func<CategoryViewModel, object>> GetSortPropertyExpression(string sortField)
        {
            switch (sortField)
            {
                case "Title":
                    return m => m.Title;
                case "Description":
                    return m => m.Description;
                case "Type":
                    return m => m.TransactionType.Description;
                case "Category":
                    return m => m.ParentTransactionCategory.Title;
                default:
                    return m => m.Title;
            }
        }

        protected override Expression<Func<CategoryViewModel, object>> GetIncludePropertiesExpression()
        {
            return m => new
            {
                m.Title,
                m.Description,
                Type = m.TransactionType.Description,
                ParentTransactionCategoryTitle = m.ParentTransactionCategory.Title
            };
        }

        protected override List<CategoryListViewModel> FormatData(IEnumerable<CategoryViewModel> data)
        {
            return data.Select(item => new CategoryListViewModel
            {
                Id = item.Id,
                Icon = GetCategoryBadge(item.Title, item.Color, item.Icon),
                Title = item.Title,
                Description = item.Description,
                Type = item.TransactionType.Description,
                ParentTransactionCategoryTitle = GetCategoryBadge(item.ParentTransactionCategory),
                Actions = item.Id
            }).ToList();
        }

        private static string GetCategoryBadge(CategoryDto category)
        {
            if (category == null)
                return string.Empty;

            return GetCategoryBadge(category.Title, category.Color, category.Icon);
        }

        private static string GetCategoryBadge(string title, string color, string icon)
        {
            if (string.IsNullOrEmpty(color))
            {
                return title;
            }

            return $"<div class=\"d-flex align-items-center\"><span class=\"btn btn-icon fs-5\" style=\"background-color:{color}; color: #fff; cursor: default\">{icon}</span><span class=\"ms-2\">{title}</span></div>";
        }
    }
}
