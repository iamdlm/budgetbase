using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Tabulator
{
    public class TabulatorSorter
    {
        [Display(Name = "column")]
        [JsonPropertyName("column")]
        public string Column { get; set; }

        [Display(Name = "Field")]
        [JsonPropertyName("Field")]
        public string Field { get; set; }

        [Display(Name = "dir")]
        [JsonPropertyName("dir")]
        public string Dir { get; set; }
    }
}
