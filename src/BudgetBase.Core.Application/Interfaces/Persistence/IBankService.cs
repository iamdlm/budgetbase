using BudgetBase.Core.Application.DTOs.Persistence;

namespace BudgetBase.Core.Application.Interfaces.Persistence
{
    public interface IBankService
    {
        Task<BankDto> GetByIdAsync(Guid id);
        IEnumerable<BankDto> GetByCountryId(Guid countryId);
        Task<IEnumerable<BankDto>> GetAllAsync();
    }
}
