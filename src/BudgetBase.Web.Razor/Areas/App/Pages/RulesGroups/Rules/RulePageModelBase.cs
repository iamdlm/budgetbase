using AutoMapper;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Core.Application.Interfaces.Identity;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Rules
{
    public abstract class RulePageModelBase : BreadcrumbPageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly ITransactionRuleFieldService _ruleFieldService;
        private readonly ITransactionRuleConditionService _ruleConditionService;

        protected RulePageModelBase(
            ICategoryService categoryService,
            ITransactionRuleFieldService ruleFieldService,
            ITransactionRuleConditionService ruleConditionService,
            IUserService userService) : base(userService)
        {
            _categoryService = categoryService;
            _ruleFieldService = ruleFieldService;
            _ruleConditionService = ruleConditionService;
        }

        protected async Task SetRuleFieldsSelectListAsync(object selectedRuleFieldId = null)
        {
            var fields = await _ruleFieldService.GetAllAsync().ConfigureAwait(false);
            ViewData["TransactionRuleFieldId"] = new SelectList(fields, "Id", "Description", selectedRuleFieldId);
        }

        protected async Task SetRuleConditionsSelectListAsync(object selectedRuleConditionId = null)
        {
            var conditions = await _ruleConditionService.GetAllAsync().ConfigureAwait(false);
            ViewData["TransactionRuleConditionId"] = new SelectList(conditions, "Id", "Description", selectedRuleConditionId);
        }

        protected async Task SetSelectListsAsync()
        {
            await SetRuleFieldsSelectListAsync().ConfigureAwait(false);
            await SetRuleConditionsSelectListAsync().ConfigureAwait(false);
        }
    }
}
