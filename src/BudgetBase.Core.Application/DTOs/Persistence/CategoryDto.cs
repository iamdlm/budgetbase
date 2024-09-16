using BudgetBase.Core.Application.Interfaces.Persistence;

namespace BudgetBase.Core.Application.DTOs.Persistence
{
    public class CategoryDto : IIdentifiable
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; }

        public string? Color { get; set; }

        public Guid TransactionTypeId { get; set; }

        public virtual EnumDto TransactionType { get; set; }

        public Guid? ParentTransactionCategoryId { get; set; }

        public virtual CategoryDto? ParentTransactionCategory { get; set; }
    }
}
