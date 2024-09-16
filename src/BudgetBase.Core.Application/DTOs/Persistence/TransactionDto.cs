using BudgetBase.Core.Application.Interfaces.Persistence;

namespace BudgetBase.Core.Application.DTOs.Persistence
{
    public class TransactionDto : IIdentifiable
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public Guid TransactionEntryTypeId { get; set; }

        public virtual EnumDto TransactionEntryType { get; set; }

        public Guid? RecurrencyTypeId { get; set; }

        public virtual EnumDto? RecurrencyType { get; set; }

        public string? Description { get; set; }

        public Guid SourceAccountId { get; set; }

        public Guid? TargetAccountId { get; set; }

        public Guid? TransactionCategoryId { get; set; }


        public virtual AccountDto? SourceAccount { get; set; } = default!;

        public virtual AccountDto? TargetAccount { get; set; }

        public virtual CategoryDto? TransactionCategory { get; set; }
    }
}
