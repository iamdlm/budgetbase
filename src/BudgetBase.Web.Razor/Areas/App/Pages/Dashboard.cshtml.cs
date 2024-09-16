using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BudgetBase.Web.Razor.Areas.App.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IDashboardService _dashboardService;
        private readonly ITransactionService _transactionService;

        public DashboardModel(IDashboardService dashboardService, ITransactionService transactionService)
        {
            _dashboardService = dashboardService;
            _transactionService = transactionService;
        }

        public async Task<JsonResult> OnPostGetIncomeAsync(string period)
        {
            ChartTimeSeries timeSeries = GetPeriodDates(period);
            DateTime startDateTime = DateTime.SpecifyKind(Convert.ToDateTime(timeSeries.StartDate), DateTimeKind.Utc);
            DateTime endDateTime = DateTime.SpecifyKind(Convert.ToDateTime(timeSeries.EndDate), DateTimeKind.Utc);

            decimal result = await _dashboardService.GetTotalIncomeAsync(startDateTime, endDateTime).ConfigureAwait(false);
            return new JsonResult(result);
        }

        public async Task<JsonResult> OnPostGetExpensesAsync(string period)
        {
            ChartTimeSeries timeSeries = GetPeriodDates(period);
            DateTime startDateTime = DateTime.SpecifyKind(Convert.ToDateTime(timeSeries.StartDate), DateTimeKind.Utc);
            DateTime endDateTime = DateTime.SpecifyKind(Convert.ToDateTime(timeSeries.EndDate), DateTimeKind.Utc);

            decimal result = await _dashboardService.GetTotalExpensesAsync(startDateTime, endDateTime).ConfigureAwait(false);
            return new JsonResult(result);
        }

        public async Task<JsonResult> OnPostGetTransfersAsync(string period)
        {
            ChartTimeSeries timeSeries = GetPeriodDates(period);
            DateTime startDateTime = DateTime.SpecifyKind(Convert.ToDateTime(timeSeries.StartDate), DateTimeKind.Utc);
            DateTime endDateTime = DateTime.SpecifyKind(Convert.ToDateTime(timeSeries.EndDate), DateTimeKind.Utc);

            decimal result = await _dashboardService.GetTotalTransfersAsync(startDateTime, endDateTime).ConfigureAwait(false);
            return new JsonResult(result);
        }

        public JsonResult OnPostGetUncategorized(string period)
        {
            ChartTimeSeries timeSeries = GetPeriodDates(period);
            DateTime startDateTime = DateTime.SpecifyKind(Convert.ToDateTime(timeSeries.StartDate), DateTimeKind.Utc);
            DateTime endDateTime = DateTime.SpecifyKind(Convert.ToDateTime(timeSeries.EndDate), DateTimeKind.Utc);

            decimal result = _dashboardService.GetTotalUncategorized(startDateTime, endDateTime);
            return new JsonResult(result);
        }

        public Task<JsonResult> OnPostGetPLDataAsync(string period)
        {
            List<ChartPLOptionsCategory> categories = GetPeriodCategories(period);

            ChartPLOptions data = _dashboardService.GetChartData(categories);

            return Task.FromResult(new JsonResult(data));
        }

        public async Task<JsonResult> OnPostGetPieDataAsync(string period, string type)
        {
            ChartTimeSeries timeSeries = GetPeriodDates(period);
            DateTime startDateTime = DateTime.SpecifyKind(Convert.ToDateTime(timeSeries.StartDate), DateTimeKind.Utc);
            DateTime endDateTime = DateTime.SpecifyKind(Convert.ToDateTime(timeSeries.EndDate), DateTimeKind.Utc);

            ChartPieOptions data = await _dashboardService.GetTotalTransactionsByCategoryAsync(startDateTime, endDateTime, type).ConfigureAwait(false);
            return new JsonResult(data);
        }

        public async Task<JsonResult> OnPostGetRecurringDataAsync(string type)
        {
            Dictionary<string, decimal> data = await _dashboardService.GetRecurringTransactionsAsync(type).ConfigureAwait(false);

            return new JsonResult(data);
        }

        private static ChartTimeSeries GetPeriodDates(string selectedPeriod)
        {
            DateTime now = DateTime.Today, endDate = now;
            DateTime startDate;

            switch (selectedPeriod.ToLower())
            {
                case "month":
                    startDate = new DateTime(endDate.Year, endDate.Month, 1);
                    break;
                case "quarter":
                    int monthStartOfQuarter = ((endDate.Month - 1) / 3) * 3 + 1;
                    startDate = new DateTime(endDate.Year, monthStartOfQuarter, 1);
                    break;
                case "semester":
                    // Assuming January and July as the start months of semesters
                    int monthStartOfSemester = endDate.Month <= 6 ? 1 : 7;
                    startDate = new DateTime(endDate.Year, monthStartOfSemester, 1);
                    break;
                case "year":
                    startDate = new DateTime(endDate.Year, 1, 1);
                    break;
                case "all":
                    startDate = new DateTime(1990, 1, 1);
                    break;
                default:
                    throw new ArgumentException("Invalid time period specified.");
            }

            SetDateTimeKind(ref endDate, ref startDate);

            return new ChartTimeSeries()
            {
                StartDate = startDate,
                EndDate = endDate,
            };
        }

        private List<ChartPLOptionsCategory> GetPeriodCategories(string selectedPeriod)
        {
            var categories = new List<ChartPLOptionsCategory>();
            DateTime now = DateTime.Today;
            DateTime startDate, endDate;

            switch (selectedPeriod.ToLower())
            {
                case "month":
                    startDate = new DateTime(now.Year, now.Month, 1);
                    endDate = startDate.AddMonths(1).AddDays(-1);
                    SetDateTimeKind(ref endDate, ref startDate);
                    categories.Add(new ChartPLOptionsCategory { StartDate = startDate, EndDate = endDate, Name = CultureInfo.InvariantCulture.DateTimeFormat.GetAbbreviatedMonthName(now.Month) });
                    break;
                case "quarter":
                    int quarterStartMonth = ((now.Month - 1) / 3) * 3 + 1;
                    for (int i = 0; i < 3; i++)
                    {
                        startDate = new DateTime(now.Year, quarterStartMonth + i, 1);
                        endDate = startDate.AddMonths(1).AddDays(-1);
                        SetDateTimeKind(ref endDate, ref startDate);
                        categories.Add(new ChartPLOptionsCategory { StartDate = startDate, EndDate = endDate, Name = CultureInfo.InvariantCulture.DateTimeFormat.GetAbbreviatedMonthName(quarterStartMonth + i) });
                    }
                    break;
                case "semester":
                    int semesterStartMonth = now.Month <= 6 ? 1 : 7;
                    for (int i = 0; i < 6; i++)
                    {
                        startDate = new DateTime(now.Year, semesterStartMonth + i, 1);
                        endDate = startDate.AddMonths(1).AddDays(-1);
                        SetDateTimeKind(ref endDate, ref startDate);
                        categories.Add(new ChartPLOptionsCategory { StartDate = startDate, EndDate = endDate, Name = CultureInfo.InvariantCulture.DateTimeFormat.GetAbbreviatedMonthName(semesterStartMonth + i) });
                    }
                    break;
                case "year":
                    for (int i = 0; i < 12; i++)
                    {
                        startDate = new DateTime(now.Year, 1 + i, 1);
                        endDate = startDate.AddMonths(1).AddDays(-1);
                        SetDateTimeKind(ref endDate, ref startDate);
                        categories.Add(new ChartPLOptionsCategory { StartDate = startDate, EndDate = endDate, Name = CultureInfo.InvariantCulture.DateTimeFormat.GetAbbreviatedMonthName(1 + i) });
                    }
                    break;
                case "all":
                    int firstYear = _transactionService.GetFirstTransaction().Year;
                    int currentYear = now.Year;
                    for (int year = firstYear; year <= currentYear; year++)
                    {
                        startDate = new DateTime(year, 1, 1);
                        endDate = new DateTime(year, 12, 31);
                        SetDateTimeKind(ref endDate, ref startDate);
                        categories.Add(new ChartPLOptionsCategory { StartDate = startDate, EndDate = endDate, Name = year.ToString() });
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid time period specified.");
            }

            return categories;
        }

        private static void SetDateTimeKind(ref DateTime endDate, ref DateTime startDate)
        {
            startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
            endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);
        }
    }
}
