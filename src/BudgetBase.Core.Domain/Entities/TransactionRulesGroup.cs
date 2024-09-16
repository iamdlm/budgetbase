namespace BudgetBase.Core.Domain.Entities
{
    public class TransactionRulesGroup : BaseAuditableEntity
    {
        public string Name { get; set; }

        public Guid TransactionCategoryId { get; set; }

        public virtual TransactionCategory TransactionCategory { get; set; }

        public virtual ICollection<TransactionRule> TransactionRules { get; set; }

        public Guid TransactionRulesGroupOperatorId { get; set; }

        public TransactionRulesGroupOperator TransactionRulesGroupOperator { get; set; }
    }
}
