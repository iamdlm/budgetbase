using BudgetBase.Core.Application.DTOs.Persistence;

namespace BudgetBase.Core.Application.Interfaces.Persistence
{
    public interface ITransactionService : IBaseService<TransactionDto>
    {
        DateTime GetFirstTransaction();
    }
}
