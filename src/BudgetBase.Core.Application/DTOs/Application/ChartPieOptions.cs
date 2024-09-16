namespace BudgetBase.Core.Application.DTOs.Application
{
    public class ChartPieOptions
    {
        public List<decimal> Series { get; set; } = new List<decimal>();
        public List<string> Labels { get; set; } = new List<string>();
        public List<string> Colors { get; set; } = new List<string>();
    }
}