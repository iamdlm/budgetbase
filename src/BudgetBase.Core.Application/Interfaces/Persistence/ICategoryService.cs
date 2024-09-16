using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.DTOs.Persistence;

namespace BudgetBase.Core.Application.Interfaces.Persistence
{
    public interface ICategoryService : IBaseService<CategoryDto>
    {
        IEnumerable<CategoryDto> GetByType(Guid typeId);
    }
}
