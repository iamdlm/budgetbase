using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels
{
    public class BaseEntityViewModel
    {
        [JsonIgnore]
        [Display(Name = "Id")]
        [JsonPropertyName("Id")]
        public string? Id { get; set; }
    }
}
