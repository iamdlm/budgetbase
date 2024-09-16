namespace BudgetBase.Core.Domain.Entities
{
    public class TransactionRule : BaseAuditableEntity
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public Guid TransactionRuleFieldId { get; set; }

        public TransactionRuleField TransactionRuleField { get; set; }

        public Guid TransactionRuleConditionId { get; set; }

        public TransactionRuleCondition TransactionRuleCondition { get; set; }

        public Guid TransactionRulesGroupId { get; set; }

        public virtual TransactionRulesGroup TransactionRulesGroup { get; set; }
    }
}
