using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Tabulator
{
    public class EntityListViewModel : BaseEntityViewModel
    {
        [Display(Name = "Actions")]
        [JsonPropertyName("Actions")]
        public string? Actions { get; set; }
    }
}
