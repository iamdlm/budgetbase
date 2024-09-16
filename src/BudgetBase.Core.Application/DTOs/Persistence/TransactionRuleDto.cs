using BudgetBase.Core.Application.Interfaces.Persistence;

namespace BudgetBase.Core.Application.DTOs.Persistence
{
    public class TransactionRuleDto : IIdentifiable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public Guid TransactionRuleFieldId { get; set; }

        public virtual EnumDto TransactionRuleField { get; set; }

        public Guid TransactionRuleConditionId { get; set; }

        public virtual EnumDto TransactionRuleCondition { get; set; }

        public Guid TransactionRulesGroupId { get; set; }

        public virtual TransactionRulesGroupDto TransactionRulesGroup { get; set; }
    }
}
