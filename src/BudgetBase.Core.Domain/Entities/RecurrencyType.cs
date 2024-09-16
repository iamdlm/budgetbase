namespace BudgetBase.Core.Domain.Entities
{
    public class RecurrencyType : Enum
    {
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}
