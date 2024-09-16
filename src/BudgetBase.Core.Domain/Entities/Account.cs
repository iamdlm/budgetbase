using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetBase.Core.Domain.Entities
{
    public class Account : BaseAuditableEntity
    {
        #region Properties

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string IBAN { get; set; }

        #endregion

        #region Foreign Keys

        public virtual ICollection<Transaction>? SourceAccountTransactions { get; set; }

        public virtual ICollection<Transaction>? TargetAccountTransactions { get; set; }

        public virtual ICollection<Import>? SourceAccountImports { get; set; }

        #endregion
    }
}
