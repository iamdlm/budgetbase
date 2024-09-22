using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Categories;
using BudgetBase.Web.Razor.Helpers;
using BudgetBase.Core.Application.Interfaces.Identity;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Categories
{
    public class CategoryEditModel : CategoryPageModelBase
    {
        private readonly IMapper _mapper;

        public CategoryEditModel(
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await SetParentCategoryIdSelectListAsync(Category.ParentTransactionCategoryId).ConfigureAwait(false);
                await SetTransactionTypeSelectListAsync(Category.TransactionTypeId).ConfigureAwait(false);

                return Page();
            }

            try
            {
                CategoryDto dto = _mapper.Map<CategoryDto>(Category);
                await _categoryService.UpdateAsync(dto).ConfigureAwait(false);
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
