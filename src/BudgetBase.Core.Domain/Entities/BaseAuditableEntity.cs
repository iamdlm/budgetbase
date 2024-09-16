using BudgetBase.Core.Domain.Interfaces;

namespace BudgetBase.Core.Domain.Entities
{
    public abstract class BaseAuditableEntity : BaseEntity, IBaseAuditableEntity
    {
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
