using BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Tabulator;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.RulesGroup
{
    public class RulesGroupListViewModel : EntityListViewModel
    {
        [Display(Name = "Name")]
        [JsonPropertyName("Name")]
        public string Name { get; set; } = default!;

        [Display(Name = "Rules")]
        [JsonPropertyName("Rules")]
        public int RulesCount { get; set; }

        [Display(Name = "Category")]
        [JsonPropertyName("Category")]
        public string Category { get; set; }

        [Display(Name = "Operator")]
        [JsonPropertyName("Operator")]
        public string Operator { get; set; }
    }
}
