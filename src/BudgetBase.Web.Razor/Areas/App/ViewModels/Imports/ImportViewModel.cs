using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Tabulator;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.Imports
{
    public class ImportViewModel : EntityListViewModel
    {
        [Required(ErrorMessage = "The field is required.")]
        [Display(Name = "Country")]
        [JsonPropertyName("Country")]
        public Guid? CountryId { get; set; }

        public virtual CountryDto? Country { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        [Display(Name = "Bank")]
        [JsonPropertyName("Bank")]
        public Guid? BankId { get; set; }

        public virtual BankDto? Bank { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        [Display(Name = "Source Account")]
        [JsonPropertyName("Source Account")]
        public Guid? SourceAccountId { get; set; }

        public virtual AccountDto? SourceAccount { get; set; }

        public IFormFile? File { get; set; }

        public string? FileName { get; set; }

        [Display(Name = "Insert duplicates")]
        [JsonPropertyName("Insert duplicates")]
        public bool InsertDuplicates { get; set; }

        [Display(Name = "Ignore rules")]
        [JsonPropertyName("Ignore rules")]
        public bool IgnoreRules { get; set; }

        public DateTime CreatedOn { get; set; }

        [Display(Name = "Processed")]
        [JsonPropertyName("Processed")]
        public int TransactionsCount { get; set; }

        [Display(Name = "Inserted")]
        [JsonPropertyName("Inserted")]
        public int TransactionsInserted { get; set; }
    }
}
