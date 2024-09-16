using BudgetBase.Core.Application.DTOs.Persistence;

namespace BudgetBase.Core.Application.Interfaces.Persistence
{
    public interface IImportService : IBaseService<ImportDto>
    {
        Task ImportAsync(ImportDto importDto, List<TransactionDto> transactions, Guid? sourceAccountId = null);
        Task DeleteImportAsync(Guid id);
    }
}
