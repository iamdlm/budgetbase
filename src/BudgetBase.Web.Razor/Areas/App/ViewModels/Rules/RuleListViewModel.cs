using BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Tabulator;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.Rules
{
    public class RuleListViewModel : EntityListViewModel
    {
        [Display(Name = "Name")]
        [JsonPropertyName("Name")]
        public string Name { get; set; } = default!;

        [Display(Name = "Value")]
        [JsonPropertyName("Value")]
        public string Value { get; set; } = default!;

        [Display(Name = "Field")]
        [JsonPropertyName("Field")]
        public string RuleField { get; set; }

        [Display(Name = "Condition")]
        [JsonPropertyName("Condition")]
        public string RuleCondition { get; set; }
    }
}
