using BudgetBase.Core.Application.DTOs.Application;
using System.Linq.Expressions;

namespace BudgetBase.Core.Application.Interfaces.Persistence
{
    public interface IBaseService<TDto> where TDto : class, IIdentifiable
    {
        Task<PaginatedResult<TDto>> GetPaginatedAsync(int pageNumber, int pageSize, string search, string sortProperty, string sortDirection, List<string> visibleColumns, List<string> includeProperties);
        Task<TDto> GetByIdAsync(Guid id);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task CreateAsync(TDto dto);
        Task UpdateAsync(TDto dto);
        Task DeleteAsync(Guid id);
    }
}
