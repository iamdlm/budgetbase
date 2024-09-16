using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Tabulator;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.Imports
{
    public class ImportListViewModel : EntityListViewModel
    {
        [Display(Name = "Bank")]
        [JsonPropertyName("Bank")]
        public string BankName { get; set; }

        [Display(Name = "Source Account")]
        [JsonPropertyName("Source Account")]
        public string SourceAccountName { get; set; }

        [Display(Name = "Created On")]
        [JsonPropertyName("Created On")]
        public string Date { get; set; }

        [Display(Name = "File Name")]
        [JsonPropertyName("File Name")]
        public string FileName { get; set; }

        [Display(Name = "Processed")]
        [JsonPropertyName("Processed")]
        public int TransactionsCount { get; set; }

        [Display(Name = "Inserted")]
        [JsonPropertyName("Inserted")]
        public int TransactionsInserted { get; set; }
    }
}