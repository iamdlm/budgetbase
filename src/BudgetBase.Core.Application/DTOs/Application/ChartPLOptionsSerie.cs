using System.Text.Json.Serialization;

namespace BudgetBase.Core.Application.DTOs.Application
{
    public class ChartPLOptionsSerie
    {
        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Color { get; set; }

        public string Group { get; set; }
        
        public List<decimal> Data { get; set; }
    }
}
