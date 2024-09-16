using System.ComponentModel.DataAnnotations;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Tabulator
{
    public class TabulatorFilter
    {
        [Display(Name = "field")]
        public string Field { get; set; }

        [Display(Name = "type")]
        public string Type { get; set; }

        [Display(Name = "value")]
        public int Value { get; set; }
    }
}