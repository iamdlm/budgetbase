using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Rules;
using BudgetBase.Core.Domain.Models;
using BudgetBase.Web.Razor.Helpers;
using System.Text.RegularExpressions;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Rules
{
    public class RuleCreateModel : RulePageModelBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRuleService _ruleService;

        public RuleCreateModel(
            IMapper mapper,
            ICategoryService categoryService,
            ITransactionRuleService ruleService,
            ITransactionRuleFieldService ruleFieldService,
            ITransactionRuleConditionService ruleConditionService) : base(categoryService, ruleFieldService, ruleConditionService)
        {
            _mapper = mapper;
            _ruleService = ruleService;
        }

        public async Task<IActionResult> OnGetAsync(Guid? groupId)
        {
            Rule = new RuleViewModel();

            if (groupId.HasValue)
            {
                Rule.TransactionRulesGroupId = groupId;
            }
            await SetSelectListsAsync().ConfigureAwait(false);
            return Page();
        }

        [BindProperty]
        public RuleViewModel Rule { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Rule == null)
            {
                await SetSelectListsAsync().ConfigureAwait(false);
                return Page();
            }

            try
            {
                TransactionRuleDto dto = _mapper.Map<TransactionRuleDto>(Rule);
                await _ruleService.CreateAsync(dto).ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
                ModelState.AddValidationErrorsToModelState(ex.Errors);
                await SetSelectListsAsync().ConfigureAwait(false);
                return Page();
            }

            return RedirectToPage("./Index", new { groupId = Rule.TransactionRulesGroupId.Value });
        }
    }
}
