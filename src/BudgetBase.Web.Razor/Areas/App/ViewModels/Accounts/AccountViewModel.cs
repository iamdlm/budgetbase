using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Tabulator;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.Accounts
{
    public class AccountViewModel : EntityListViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Number")]
        [JsonPropertyName("Number")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "IBAN")]
        [JsonPropertyName("IBAN")]
        public string IBAN { get; set; }
    }
}
