using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.RulesGroup;
using BudgetBase.Web.Razor.Helpers;

namespace BudgetBase.Web.Razor.Areas.App.Pages.RulesGroups
{
    public class RulesGroupCreateModel : RulesGroupPageModelBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRulesGroupService _rulesGroupService;

        public RulesGroupCreateModel(
            IMapper mapper,
            ITransactionRulesGroupService rulesGroupService,
            ICategoryService categoryService,
            ITransactionRulesGroupOperatorService operatorService) : base(categoryService, operatorService)
        {
            _mapper = mapper;
            _rulesGroupService = rulesGroupService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await SetSelectListsAsync().ConfigureAwait(false);
            return Page();
        }

        [BindProperty]
        public RulesGroupViewModel RulesGroup { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || RulesGroup == null)
            {
                await SetSelectListsAsync().ConfigureAwait(false);
                return Page();
            }

            try
            {
                TransactionRulesGroupDto dto = _mapper.Map<TransactionRulesGroupDto>(RulesGroup);
                await _rulesGroupService.CreateAsync(dto).ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
                ModelState.AddValidationErrorsToModelState(ex.Errors);
                await SetSelectListsAsync().ConfigureAwait(false);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
