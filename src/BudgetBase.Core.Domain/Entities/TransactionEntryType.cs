namespace BudgetBase.Core.Domain.Entities
{
    public class TransactionEntryType : Enum
    {
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}
