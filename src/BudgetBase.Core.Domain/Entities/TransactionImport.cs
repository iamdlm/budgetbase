namespace BudgetBase.Core.Domain.Entities
{
    public class TransactionImport
    {
        public Guid TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }

        public Guid ImportId { get; set; }
        public virtual Import Import { get; set; }
    }
}
