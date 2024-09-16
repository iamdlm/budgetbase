using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetBase.Core.Domain.Entities
{
    public class Import : BaseAuditableEntity
    {
        #region Properties

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool InsertDuplicates { get; set; }

        [Required]
        public bool IgnoreRules { get; set; }

        [Required]
        public string Filename { get; set; }

        [Required]
        public int TransactionsCount { get; set; }

        [Required]
        public int TransactionsInserted { get; set; }

        #endregion

        #region Foreign Keys

        #region Bank

        public Guid? BankId { get; set; }

        public virtual Bank? Bank { get; set; }

        #endregion

        #region TransactionImports

        public virtual ICollection<TransactionImport> TransactionImports { get; set; }

        #endregion

        #region Source Account

        public Guid? SourceAccountId { get; set; }

        [ForeignKey("SourceAccountId")]
        [InverseProperty("SourceAccountImports")]
        public virtual Account? SourceAccount { get; set; }

        #endregion

        #endregion
    }
}
