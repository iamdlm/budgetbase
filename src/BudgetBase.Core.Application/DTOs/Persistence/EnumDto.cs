using BudgetBase.Core.Application.Interfaces.Persistence;

namespace BudgetBase.Core.Application.DTOs.Persistence
{
    public class EnumDto : IIdentifiable
    {
        public Guid Id { get; set; }

        public int Index { get; set; }
        
        public string Description { get; set; }
    }
}
