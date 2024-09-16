using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.DTOs.Persistence;

namespace BudgetBase.Core.Application.Interfaces.Persistence
{
    public interface ICountryService
    {
        Task<CountryDto> GetByIdAsync(Guid id);
        Task<IEnumerable<CountryDto>> GetAllAsync();
        Task<IEnumerable<CountryDto>> GetAllWithAssociatedBankAsync();
    }
}
