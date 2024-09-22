using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Identity;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Categories;
using BudgetBase.Web.Razor.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Categories
{
    public class CategoryDeleteModel : CategoryPageModelBase
    {
        private readonly IMapper _mapper;

        public CategoryDeleteModel(
            IMapper mapper,
            ICategoryService categoryService,
            ITransactionTypeService enumService,
            IUserService userService) : base(categoryService, enumService, userService)
        { 
            _mapper = mapper;
        }

        [BindProperty]
        public CategoryViewModel Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryDto dto = await _categoryService.GetByIdAsync(id.Value).ConfigureAwait(false);

            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                Category = _mapper.Map<CategoryViewModel>(dto);
            }

            await SetParentCategoryIdSelectListAsync(Category.ParentTransactionCategoryId).ConfigureAwait(false);
            await SetTransactionTypeSelectListAsync(Category.TransactionTypeId).ConfigureAwait(false);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _categoryService.DeleteAsync(id.Value).ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
                ModelState.AddValidationErrorsToModelState(ex.Errors);

                await SetParentCategoryIdSelectListAsync(Category.ParentTransactionCategoryId).ConfigureAwait(false);
                await SetTransactionTypeSelectListAsync(Category.TransactionTypeId).ConfigureAwait(false);

                CategoryDto dto = await _categoryService.GetByIdAsync(id.Value).ConfigureAwait(false);
                Category = _mapper.Map<CategoryViewModel>(dto);

                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
