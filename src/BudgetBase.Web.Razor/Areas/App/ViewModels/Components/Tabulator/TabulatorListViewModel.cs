using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Tabulator
{
    public class TabulatorListViewModel<T> where T : class
    {
        [Display(Name = "last_page")]
        [JsonPropertyName("last_page")]
        public int LastPage { get; set; }

        [Display(Name = "last_row")]
        [JsonPropertyName("last_row")]
        public int LastRow { get; set; }

        [Display(Name = "data")]
        [JsonPropertyName("data")]
        public List<T> Data { get; set; }
    }
}
