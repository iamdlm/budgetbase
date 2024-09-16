namespace BudgetBase.Core.Application.Interfaces.Persistence
{
    public interface IEnumService<TDto>
        where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(Guid id);
        Task<TDto> GetByNameAsync(string name);
        Task CreateAsync(TDto dto);
    }
}
