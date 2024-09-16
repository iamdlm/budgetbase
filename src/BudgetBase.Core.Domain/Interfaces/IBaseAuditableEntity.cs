namespace BudgetBase.Core.Domain.Interfaces
{
    public interface IBaseAuditableEntity
    {
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
