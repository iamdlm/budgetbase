using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Rules;
using BudgetBase.Core.Domain.Models;
using BudgetBase.Web.Razor.Helpers;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Rules
{
    public class RuleEditModel : RulePageModelBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRuleService _ruleService;

        public RuleEditModel(
            IMapper mapper,
             ICategoryService categoryService,
            ITransactionRuleService ruleService,
            ITransactionRuleFieldService ruleFieldService,
            ITransactionRuleConditionService ruleConditionService) : base(categoryService,ruleFieldService, ruleConditionService)
        {
            _mapper = mapper;
            _ruleService = ruleService;
        }

        [BindProperty]
        public RuleViewModel Rule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TransactionRuleDto dto = await _ruleService.GetByIdAsync(id.Value).ConfigureAwait(false);

            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                Rule = _mapper.Map<RuleViewModel>(dto);
            }

            await SetSelectListsAsync().ConfigureAwait(false);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await SetSelectListsAsync().ConfigureAwait(false);
                return Page();
            }

            try
            {
                TransactionRuleDto dto = _mapper.Map<TransactionRuleDto>(Rule);
                await _ruleService.UpdateAsync(dto).ConfigureAwait(false);
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
