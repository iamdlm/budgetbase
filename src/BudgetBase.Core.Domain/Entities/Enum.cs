namespace BudgetBase.Core.Domain.Entities
{
    public abstract class Enum : BaseEntity
    {
        public int Index { get; set; }

        public string Description { get; set; }
    }
}
