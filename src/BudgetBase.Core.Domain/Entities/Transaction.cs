using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetBase.Core.Domain.Entities
{
    public class Transaction : BaseAuditableEntity
    {
        #region Properties

        [Required]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        #endregion

        #region Foreign Keys

        #region Source Account

        public Guid SourceAccountId { get; set; }

        [ForeignKey("SourceAccountId")]
        [InverseProperty("SourceAccountTransactions")]
        public virtual Account? SourceAccount { get; set; }

        #endregion

        #region Target Account

        public Guid? TargetAccountId { get; set; }

        [ForeignKey("TargetAccountId")]
        [InverseProperty("TargetAccountTransactions")]
        public virtual Account? TargetAccount { get; set; }

        #endregion

        #region TransactionEntryType

        public Guid? TransactionEntryTypeId { get; set; }

        public virtual TransactionEntryType? TransactionEntryType { get; set; }

        #endregion

        #region RecurrencyType

        public Guid? RecurrencyTypeId { get; set; }

        public virtual RecurrencyType? RecurrencyType { get; set; }

        #endregion

        #region Category

        public Guid? TransactionCategoryId { get; set; }

        public virtual TransactionCategory? TransactionCategory { get; set; }

        #endregion

        #region TransactionImports

        public virtual ICollection<TransactionImport> TransactionImports { get; set; }

        #endregion

        #endregion
    }
}
