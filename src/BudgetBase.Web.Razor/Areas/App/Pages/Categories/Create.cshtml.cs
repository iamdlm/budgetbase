using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Categories;
using BudgetBase.Web.Razor.Helpers;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Categories
{
    public class CategoryCreateModel : CategoryPageModelBase
    {
        private readonly IMapper _mapper;

        public CategoryCreateModel(IMapper mapper, ICategoryService categoryService, ITransactionTypeService enumService) : base(categoryService, enumService)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await SetTransactionTypeSelectListAsync().ConfigureAwait(false);
            return Page();
        }

        [BindProperty]
        public CategoryViewModel Category { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Category == null)
            {
                await SetTransactionTypeSelectListAsync().ConfigureAwait(false);
                return Page();
            }

            try
            {
                CategoryDto dto = _mapper.Map<CategoryDto>(Category);
                await _categoryService.CreateAsync(dto).ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
                ModelState.AddValidationErrorsToModelState(ex.Errors);
                await SetTransactionTypeSelectListAsync().ConfigureAwait(false);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
