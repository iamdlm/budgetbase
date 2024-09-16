using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Tabulator;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Rules;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Rules
{
    public class RuleIndexModel : BaseIndexModel<RuleListViewModel, RuleViewModel, TransactionRuleDto>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRuleService _ruleService;

        public RuleIndexModel(IMapper mapper, ITransactionRuleService ruleService)
            : base(mapper, ruleService)
        {
            _mapper = mapper;
            _ruleService = ruleService;
        }

        public IList<RuleViewModel> Rules { get; set; } = default!;

        protected override Expression<Func<RuleViewModel, object>> GetSortPropertyExpression(string sortField)
        {
            switch (sortField)
            {
                case "Name":
                    return m => m.Name;
                case "Value":
                    return m => m.Value;
                case "Field":
                    return m => m.TransactionRuleField.Description;
                case "Condition":
                    return m => m.TransactionRuleCondition.Description;
                default:
                    return m => m.Name;
            }
        }

        protected override Expression<Func<RuleViewModel, object>> GetIncludePropertiesExpression()
        {
            return m => new
            {
                m.Name,
                m.Value,
                Field = m.TransactionRuleField.Description,
                Condition = m.TransactionRuleCondition.Description
            };
        }

        protected override List<RuleListViewModel> FormatData(IEnumerable<RuleViewModel> data)
        {
            return data.Select(item => new RuleListViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Value = item.Value,
                RuleField = item.TransactionRuleField?.Description,
                RuleCondition = item.TransactionRuleCondition?.Description,
                Actions = item.Id
            }).ToList();
        }

        public override Task<JsonResult> OnGetRemoteDataAsync(int pageNumber, int pageSize, List<Dictionary<string, string>> filters, List<Dictionary<string, string>> sorters, Guid? groupId)
        {
            string search = filters.Any() ? filters[0].FirstOrDefault(f => f.Key == "value").Value ?? string.Empty : string.Empty;
            string sortField = sorters.Any() ? sorters[0].FirstOrDefault(f => f.Key == "field").Value : string.Empty;
            string sortProperty = GetSortProperty(sortField);
            string sortDirection = sorters.Any() ? sorters[0].FirstOrDefault(f => f.Key == "dir").Value : "desc";

            var response = _ruleService.GetByRulesGroupId(pageNumber, pageSize, search, sortProperty, sortDirection, groupId.Value);

            var data = _mapper.Map<IEnumerable<RuleViewModel>>(response.Items);

            var result = new TabulatorListViewModel<RuleListViewModel>
            {
                LastPage = response.LastPage,
                LastRow = response.LastRow,
                Data = FormatData(data)
            };

            return Task.FromResult(new JsonResult(result));
        }
    }
}
