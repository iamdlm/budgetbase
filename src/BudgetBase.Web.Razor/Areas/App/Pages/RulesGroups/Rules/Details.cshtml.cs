using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Rules;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Rules
{
    public class RuleDetailsModel : RulePageModelBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRuleService _ruleService;

        public RuleDetailsModel(
            IMapper mapper,
            ICategoryService categoryService,
            ITransactionRuleService ruleService,
            ITransactionRuleFieldService ruleFieldService,
            ITransactionRuleConditionService ruleConditionService) : base(categoryService, ruleFieldService, ruleConditionService)
        {
            _mapper = mapper;
            _ruleService = ruleService;
        }

        public RuleViewModel Rule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                TransactionRuleDto dto = await _ruleService.GetByIdAsync(id.Value).ConfigureAwait(false);

                if (dto == null)
                {
                    return NotFound();
                }
                else
                {
                    Rule = _mapper.Map<RuleViewModel>(dto);
                }
            }
            catch (ValidationException ex)
            {
                return NotFound();
            }

            await SetSelectListsAsync().ConfigureAwait(false);

            return Page();
        }
    }
}
