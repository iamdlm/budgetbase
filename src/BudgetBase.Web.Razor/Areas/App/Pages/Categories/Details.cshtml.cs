using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Categories;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Categories
{
    public class CategoryDetailsModel : CategoryPageModelBase
    {
        private readonly IMapper _mapper;

        public CategoryDetailsModel(IMapper mapper, ICategoryService categoryService, ITransactionTypeService enumService) : base(categoryService, enumService)
        {
            _mapper = mapper;
        }

        public CategoryViewModel Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                CategoryDto dto = await _categoryService.GetByIdAsync(id.Value).ConfigureAwait(false);

                if (dto == null)
                {
                    return NotFound();
                }
                else
                {
                    Category = _mapper.Map<CategoryViewModel>(dto);
                }
            }
            catch (ValidationException ex)
            {
                return NotFound();
            }

            await SetParentCategoryIdSelectListAsync(Category.ParentTransactionCategoryId).ConfigureAwait(false);
            await SetTransactionTypeSelectListAsync(Category.TransactionTypeId).ConfigureAwait(false);
            return Page();
        }
    }
}
