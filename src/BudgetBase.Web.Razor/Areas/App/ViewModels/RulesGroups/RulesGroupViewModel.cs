using BudgetBase.Core.Application.DTOs.Persistence;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.RulesGroup
{
    public class RulesGroupViewModel : BaseEntityViewModel
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        [Display(Name = "Category")]
        [JsonPropertyName("Category")]
        public Guid? TransactionCategoryId { get; set; }

        public virtual CategoryDto? TransactionCategory { get; set; }

        [Required]
        [Display(Name = "Operator")]
        [JsonPropertyName("Operator")]
        public Guid? TransactionRulesGroupOperatorId { get; set; }

        public virtual EnumDto? TransactionRulesGroupOperator { get; set; }
    }
}
