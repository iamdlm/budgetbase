using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Domain.Entities;

namespace BudgetBase.Core.Application.Interfaces.Persistence
{
    public interface ITransactionRuleService : IBaseService<TransactionRuleDto>
    {
        PaginatedResult<TransactionRuleDto> GetByRulesGroupId(int pageNumber, int pageSize, string search, string sortProperty, string sortDirection, Guid groupId);
    }
}
