using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetBase.Core.Domain.Entities
{
    public class TransactionCategory : BaseAuditableEntity
    {
        #region Properties

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; }

        public string? Color { get; set; }

        #endregion

        #region Foreign Keys

        #region TransactionType

        public Guid? TransactionTypeId { get; set; }

        public virtual TransactionType? TransactionType { get; set; }

        #endregion

        #region Parent TransactionCategory

        [ForeignKey("TransactionCategory")]
        public Guid? ParentTransactionCategoryId { get; set; }

        public virtual TransactionCategory? ParentTransactionCategory { get; set; }

        #endregion

        #region Children TransactionCategory

        public virtual ICollection<TransactionCategory>? ChildrenTransactionCategory { get; set; }

        #endregion

        #region Transaction

        public virtual ICollection<Transaction>? Transactions { get; set; }

        #endregion

        #endregion
    }
}
