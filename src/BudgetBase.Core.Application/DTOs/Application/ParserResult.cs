using BudgetBase.Core.Application.DTOs.Persistence;

namespace BudgetBase.Core.Application.DTOs.Application
{
    public class ParserResult
    {
        public List<TransactionDto> Transactions;

        public int ProcessedCount { get; set; }
    }
}
