using BudgetBase.Core.Application.DTOs.Application;

namespace BudgetBase.Core.Application.Interfaces.Persistence
{
    public interface IDashboardService
    {
        Task<decimal> GetTotalIncomeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalExpensesAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalTransfersAsync(DateTime startDate, DateTime endDate);
        decimal GetTotalUncategorized(DateTime startDate, DateTime endDate);
        ChartPLOptions GetChartData(List<ChartPLOptionsCategory> categories);
        Task<ChartPieOptions> GetTotalTransactionsByCategoryAsync(DateTime startDate, DateTime endDate, string type);
        Task<Dictionary<string, decimal>> GetRecurringTransactionsAsync(string type);
    }
}
