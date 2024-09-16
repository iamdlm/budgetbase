using BudgetBase.Core.Application.Interfaces.Persistence;

namespace BudgetBase.Core.Application.DTOs.Persistence
{
    public class BankDto : IIdentifiable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CountryId { get; set; }

        public virtual CountryDto? Country { get; set; }
    }
}
