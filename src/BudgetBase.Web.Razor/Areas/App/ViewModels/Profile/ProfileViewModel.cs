using BudgetBase.Core.Application.DTOs.Persistence;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.Profile
{
    public class ProfileViewModel
    {
        [Display(Name = "Username")]
        [JsonPropertyName("Username")]
        public string? UserName { get; set; }

        [Display(Name = "Full name")]
        [JsonPropertyName("Full name")]
        public string? FullName { get; set; }

        [Display(Name = "Country")]
        [JsonPropertyName("Country")]
        public Guid? CountryId { get; set; }

        public virtual CountryDto? Country { get; set; }

        [Display(Name = "Occupation")]
        [JsonPropertyName("Occupation")]
        public string? Occupation { get; set; }
    }
}
