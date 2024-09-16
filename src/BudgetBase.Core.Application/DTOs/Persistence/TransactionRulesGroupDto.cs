using BudgetBase.Core.Application.Interfaces.Persistence;

namespace BudgetBase.Core.Application.DTOs.Persistence
{
    public class TransactionRulesGroupDto : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? TransactionCategoryId { get; set; }
        public virtual CategoryDto? TransactionCategory { get; set; }
        public virtual ICollection<TransactionRuleDto> TransactionRules { get; set; }
        public Guid? TransactionRulesGroupOperatorId { get; set; }
        public virtual EnumDto? TransactionRulesGroupOperator { get; set; }
    }
}
