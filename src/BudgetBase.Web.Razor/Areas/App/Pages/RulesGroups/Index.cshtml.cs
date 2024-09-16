using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Web.Razor.Areas.App.ViewModels.RulesGroup;
using System.Linq.Expressions;

namespace BudgetBase.Web.Razor.Areas.App.Pages.RulesGroups
{
    public class RulesGroupIndexModel : BaseIndexModel<RulesGroupListViewModel, RulesGroupViewModel, TransactionRulesGroupDto>
    {
        private ITransactionRulesGroupService _rulesGroupService;

        public RulesGroupIndexModel(
            IMapper mapper,
            ITransactionRulesGroupService rulesGroupService)
            : base(mapper, rulesGroupService)
        {
            _rulesGroupService = rulesGroupService;
        }

        public IList<RulesGroupViewModel> Rules { get; set; } = default!;

        protected override Expression<Func<RulesGroupViewModel, object>> GetSortPropertyExpression(string sortField)
        {
            switch (sortField)
            {
                case "Name":
                    return m => m.Name;
                case "Category":
                    return m => m.TransactionCategory.Description;
                case "Operator":
                    return m => m.TransactionRulesGroupOperator.Description;
                default:
                    return m => m.Name;
            }
        }

        protected override Expression<Func<RulesGroupViewModel, object>> GetIncludePropertiesExpression()
        {
            return m => new
            {
                m.Name,
                Category = m.TransactionCategory.Description,
                Operator = m.TransactionRulesGroupOperator.Description,
                Count = "TransactionRules"
            };
        }

        protected override List<RulesGroupListViewModel> FormatData(IEnumerable<RulesGroupViewModel> data)
        {
            return data.Select(item => new RulesGroupListViewModel
            {
                Id = item.Id,
                Name = item.Name,
                RulesCount = _rulesGroupService.GetByIdAsync(new Guid(item.Id)).Result.TransactionRules.Count,
                Category = item.TransactionCategory.Description,
                Operator = item.TransactionRulesGroupOperator.Description,
                Actions = item.Id
            }).ToList();
        }
    }
}
