using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetBase.Core.Domain.Entities
{
    public class Country : BaseEntity
    {
        #region Properties

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        #endregion

        #region Foreign Keys

        public virtual ICollection<Bank>? Banks { get; set; }

        #endregion
    }
}
