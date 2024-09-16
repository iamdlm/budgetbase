using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Interfaces.Application;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Application.Specifications;
using BudgetBase.Core.Domain.Entities;
using BudgetBase.Core.Domain.Interfaces;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public class DashboardService : IDashboardService
    {
        public IUnitOfWork _unitOfWork { get; }
        public ITransactionTypeService _transactionTypeService { get; }
        public ICurrentUserService _currentUserService { get; }
        public IRecurrencyTypeService _recurrencyTypeService { get; }

        private static readonly List<string> defaultColors = new List<string>
        {
            "#008FFB", "#00E396", "#FEB019", "#FF4560", "#775DD0",
            "#3F51B5", "#03A9F4", "#4CAF50", "#F9CE1D", "#FF9800",
            "#33B2DF", "#546E7A", "#D4526E", "#13D8AA", "#A5978B",
            "#4ECDC4", "#C7F464", "#81D4FA", "#546E7A", "#FD6A6A",
            "#2B908F", "#F9A3A4", "#90EE7E", "#FA4443", "#69D2E7",
            "#449DD1", "#F86624", "#EA3546", "#662E9B", "#C5D86D",
            "#D7263D", "#1B998B", "#2E294E", "#F46036", "#E2C044",
            "#662E9B", "#F86624", "#F9C80E", "#EA3546", "#43BCCD",
            "#5C4742", "#A5978B", "#8D5B4C", "#5A2A27", "#C4BBAF",
            "#A300D6", "#7D02EB", "#5653FE", "#2983FF", "#00B1F2"
        };
        private static Dictionary<string, int> methodIndices = new Dictionary<string, int>();
        private static readonly object lockObject = new object();


        public DashboardService(
            IUnitOfWork unitOfWork,
            ITransactionTypeService transactionTypeService,
            ICurrentUserService currentUserService,
            IRecurrencyTypeService recurrencyTypeService)
        {
            _unitOfWork = unitOfWork;
            _transactionTypeService = transactionTypeService;
            _currentUserService = currentUserService;
            _recurrencyTypeService = recurrencyTypeService;
        }

        public async Task<decimal> GetTotalIncomeAsync(DateTime startDate, DateTime endDate)
        {
            EnumDto type = await _transactionTypeService.GetByNameAsync(Domain.Models.Constants.TransactionTypes.Income).ConfigureAwait(false);

            IEnumerable<Transaction> transactions = _unitOfWork.TransactionRepo.GetWhere(t => t.TransactionCategory.TransactionTypeId == type.Id && t.Date >= startDate && t.Date <= endDate);

            return Math.Round(transactions.Sum(t => Math.Abs(t.Amount)), 2);
        }

        public async Task<decimal> GetTotalExpensesAsync(DateTime startDate, DateTime endDate)
        {
            EnumDto type = await _transactionTypeService.GetByNameAsync(Domain.Models.Constants.TransactionTypes.Expense).ConfigureAwait(false);

            IEnumerable<Transaction> transactions = _unitOfWork.TransactionRepo.GetWhere(t => t.TransactionCategory.TransactionTypeId == type.Id && t.Date >= startDate && t.Date <= endDate);

            return Math.Round(transactions.Sum(t => Math.Abs(t.Amount)), 2);
        }

        public async Task<decimal> GetTotalTransfersAsync(DateTime startDate, DateTime endDate)
        {
            EnumDto type = await _transactionTypeService.GetByNameAsync(Domain.Models.Constants.TransactionTypes.Transfer).ConfigureAwait(false);

            IEnumerable<Transaction> transactions = _unitOfWork.TransactionRepo.GetWhere(t => t.TransactionCategory.TransactionTypeId == type.Id && t.Date >= startDate && t.Date <= endDate);

            return Math.Round(transactions.Sum(t => Math.Abs(t.Amount)), 2);
        }

        public decimal GetTotalUncategorized(DateTime startDate, DateTime endDate)
        {
            IEnumerable<Transaction> transactions = _unitOfWork.TransactionRepo.GetWhere(t => t.TransactionCategory == null && t.Date >= startDate && t.Date <= endDate);

            return Math.Round(transactions.Sum(t => Math.Abs(t.Amount)), 2);
        }

        public ChartPLOptions GetChartData(List<ChartPLOptionsCategory> categories)
        {
            var ChartPLOptions = new ChartPLOptions
            {
                Xaxis = new ChartPLXAxis { Categories = categories.Select(c => c.Name).ToList() }
            };

            var allSeries = new Dictionary<string, Dictionary<string, ChartPLOptionsSerie>>();

            ISpecification<Transaction> spec = new OwnedByUserSpecification<Transaction>(_currentUserService.UserId);

            foreach (var category in categories)
            {
                List<Transaction> transactions = _unitOfWork.TransactionRepo
                    .GetAll(
                        t => t.Date >= category.StartDate && t.Date <= category.EndDate,
                        null,
                        spec,
                        t => t.TransactionCategory,
                        tp => tp.TransactionCategory.ParentTransactionCategory,
                        tc => tc.TransactionCategory.TransactionType)
                    .ToList();

                foreach (var transaction in transactions)
                {
                    TransactionCategory rootCategory = GetRootCategory(transaction.TransactionCategory);
                    string groupName = transaction.TransactionCategory?.TransactionType?.Description ?? "Unknown";
                    string categoryName = rootCategory?.Title ?? "Uncategorized";

                    if (!allSeries.ContainsKey(categoryName))
                    {
                        allSeries[categoryName] = new Dictionary<string, ChartPLOptionsSerie>();
                    }

                    if (!allSeries[categoryName].ContainsKey(groupName))
                    {
                        allSeries[categoryName][groupName] = new ChartPLOptionsSerie
                        {
                            Name = categoryName,
                            Color = rootCategory?.Color,
                            Group = groupName,
                            Data = new List<decimal>(new decimal[categories.Count]) // Initialize with zeros for each category/date range
                        };
                    }

                    int index = ChartPLOptions.Xaxis.Categories.IndexOf(category.Name);
                    allSeries[categoryName][groupName].Data[index] += Math.Round(Math.Abs(transaction.Amount), 2); // Sum the amounts for the category
                }
            }

            // Flatten the dictionary to a list of series
            foreach (var categorySeries in allSeries.Values)
            {
                ChartPLOptions.Series.AddRange(categorySeries.Values);
            }

            return ChartPLOptions;
        }

        public async Task<ChartPieOptions> GetTotalTransactionsByCategoryAsync(DateTime startDate, DateTime endDate, string type)
        {
            ISpecification<Transaction> spec = new OwnedByUserSpecification<Transaction>(_currentUserService.UserId);

            EnumDto transactionType = await _transactionTypeService.GetByNameAsync(type).ConfigureAwait(false);

            List<Transaction> transactions = _unitOfWork.TransactionRepo
                .GetAll(
                    t => t.Date >= startDate && t.Date <= endDate && t.TransactionCategory.TransactionTypeId == transactionType.Id,
                    null,
                    spec,
                    t => t.TransactionCategory,
                    tp => tp.TransactionCategory.ParentTransactionCategory,
                    tc => tc.TransactionCategory.TransactionType)
                .ToList();

            // Group transactions by root category and sum their amounts
            var categorySums = transactions
                .GroupBy(t => GetRootCategory(t.TransactionCategory))
                .Select(g => new
                {
                    CategoryName = g.Key?.Title ?? "Uncategorized",
                    Sum = Math.Round(g.Sum(t => Math.Abs(t.Amount)), 2),
                    Color = g.Key?.Color ?? GetNextColor("PieChart")
                })
                .ToList();

            ChartPieOptions chartPieOptions = new ChartPieOptions();

            // Populate the ChartPieOptions object
            chartPieOptions.Series.AddRange(categorySums.Select(cs => cs.Sum));
            chartPieOptions.Labels.AddRange(categorySums.Select(cs => cs.CategoryName));
            chartPieOptions.Colors.AddRange(categorySums.Select(cs => cs.Color));

            return chartPieOptions;
        }

        public async Task<Dictionary<string, decimal>> GetRecurringTransactionsAsync(string type)
        {
            ISpecification<Transaction> spec = new OwnedByUserSpecification<Transaction>(_currentUserService.UserId);

            EnumDto transactionType = await _transactionTypeService.GetByNameAsync(type).ConfigureAwait(false);

            IEnumerable<EnumDto> recurringPeriods = await _recurrencyTypeService.GetAllAsync().ConfigureAwait(false);

            Dictionary<string, decimal> recurringTransactions = new();

            foreach (EnumDto recurringPeriod in recurringPeriods.OrderBy(o => o.Index))
            {
                List<Transaction> transactions = _unitOfWork.TransactionRepo
                    .GetAll(
                        t => t.RecurrencyTypeId == recurringPeriod.Id && t.TransactionCategory.TransactionTypeId == transactionType.Id,
                        null,
                        spec,
                        t => t.TransactionCategory,
                        tp => tp.TransactionCategory.ParentTransactionCategory,
                        tc => tc.TransactionCategory.TransactionType)
                    .ToList();

                recurringTransactions.Add(recurringPeriod.Description, Math.Round(transactions.Sum(t => Math.Abs(t.Amount)), 2));
            }

            return recurringTransactions;
        }

        private TransactionCategory GetRootCategory(TransactionCategory category)
        {
            if (category?.ParentTransactionCategory == null)
            {
                return category;
            }

            return GetRootCategory(category.ParentTransactionCategory);
        }

        private static string GetNextColor(string methodId)
        {
            lock (lockObject)
            {
                // Ensure the method has an entry in the dictionary
                if (!methodIndices.ContainsKey(methodId))
                {
                    methodIndices[methodId] = 0;
                }

                // Get the color index for this method
                int index = methodIndices[methodId];

                // Check if more predefined colors are available
                if (index < defaultColors.Count)
                {
                    methodIndices[methodId]++;  // Increment index for next call
                    return defaultColors[index];
                }
            }

            // Generate a random color if all predefined ones are exhausted
            return GenerateRandomColor();
        }

        private static string GenerateRandomColor()
        {
            Random random = new();
            return $"#{random.Next(0x1000000):X6}";
        }
    }
}
