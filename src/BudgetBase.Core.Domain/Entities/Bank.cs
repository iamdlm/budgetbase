using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BudgetBase.Core.Domain.Entities
{
    public class Bank : BaseEntity
    {
        #region Properties

        [Required]
        public string Name { get; set; }

        #endregion

        #region Foreign Keys

        #region Country

        [DisplayName("Country")]
        public Guid CountryId { get; set; }

        public virtual Country? Country { get; set; }

        #endregion

        #region Imports

        public virtual ICollection<Import>? Imports { get; set; }

        #endregion

        #endregion
    }
}
