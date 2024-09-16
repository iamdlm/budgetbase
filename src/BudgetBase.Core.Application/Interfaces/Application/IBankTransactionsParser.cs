using BudgetBase.Core.Application.DTOs.Application;

namespace BudgetBase.Core.Application.Interfaces
{
    public interface IBankTransactionParser
    {
        ParserResult ParseTransactions(MemoryStream stream);
    }
}
