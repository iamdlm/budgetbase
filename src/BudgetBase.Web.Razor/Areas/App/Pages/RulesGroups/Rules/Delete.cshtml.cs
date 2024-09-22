using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Rules;
using BudgetBase.Core.Application.Services.Persistence;
using BudgetBase.Core.Application.Interfaces.Identity;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Rules
{
    public class RuleDeleteModel : RulePageModelBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRuleService _ruleService;

        public RuleDeleteModel(
            IMapper mapper,
            ICategoryService categoryService,
            ITransactionRuleService ruleService,
            ITransactionRuleFieldService ruleFieldService,
            ITransactionRuleConditionService ruleConditionService,
            IUserService userService) : base(categoryService, ruleFieldService, ruleConditionService, userService)
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _ruleService.DeleteAsync(id.Value).ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
            }

            return RedirectToPage("./Index");
        }
    }
}
