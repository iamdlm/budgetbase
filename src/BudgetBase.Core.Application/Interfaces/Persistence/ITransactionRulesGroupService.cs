using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Domain.Entities;

namespace BudgetBase.Core.Application.Interfaces.Persistence
{
    public interface ITransactionRulesGroupService : IBaseService<TransactionRulesGroupDto>
    {
        Task ApplyRulesAsync();
        Task ApplyRulesAsync(Transaction transaction, IEnumerable<TransactionRulesGroup> rules);
    }
}
