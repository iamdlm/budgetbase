using System.ComponentModel.DataAnnotations;

namespace BudgetBase.Core.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
