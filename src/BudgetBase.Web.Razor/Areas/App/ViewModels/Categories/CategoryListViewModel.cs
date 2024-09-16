using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Tabulator;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.Categories
{
    public class CategoryListViewModel : EntityListViewModel
    {
        [Display(Name = "Icon")]
        [JsonPropertyName("Icon")]
        public string Icon { get; set; }

        [Display(Name = "Title")]
        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [Display(Name = "Parent Category")]
        [JsonPropertyName("Parent Category")]
        public string ParentTransactionCategoryTitle { get; set; }

        [Display(Name = "Type")]
        [JsonPropertyName("Type")]
        public string Type { get; set; }
    }
}