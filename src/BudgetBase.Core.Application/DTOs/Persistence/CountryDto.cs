using BudgetBase.Core.Application.Interfaces.Persistence;

namespace BudgetBase.Core.Application.DTOs.Persistence
{
    public class CountryDto : IIdentifiable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
