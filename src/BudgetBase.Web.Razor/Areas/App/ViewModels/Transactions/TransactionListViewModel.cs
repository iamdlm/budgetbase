using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Tabulator;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.Transactions
{
    public class TransactionListViewModel : EntityListViewModel
    {
        [Display(Name = "Category")]
        [JsonPropertyName("Category")]
        public string CategoryTitle { get; set; }

        [Display(Name = "Description")]
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [Display(Name = "Date")]
        [JsonPropertyName("Date")]
        public string Date { get; set; }

        [Display(Name = "Amount")]
        [JsonPropertyName("Amount")]
        public string Amount { get; set; }

        [Display(Name = "Source Account")]
        [JsonPropertyName("Source Account")]
        public string SourceAccountName { get; set; }

        [Display(Name = "Target Account")]
        [JsonPropertyName("Target Account")]
        public string TargetAccountName { get; set; }

        [Display(Name = "Entry")]
        [JsonPropertyName("Entry")]
        public string EntryType { get; set; }
    }
}