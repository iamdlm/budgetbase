namespace BudgetBase.Core.Domain.Entities
{
    public class TransactionType : Enum
    {
        public virtual ICollection<TransactionCategory>? Categories { get; set; }
    }
}
