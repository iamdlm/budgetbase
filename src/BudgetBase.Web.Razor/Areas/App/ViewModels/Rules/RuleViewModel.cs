using BudgetBase.Core.Application.DTOs.Persistence;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.Rules
{
    public class RuleViewModel : BaseEntityViewModel
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Value { get; set; } = default!;

        [Required]
        [Display(Name = "Field")]
        [JsonPropertyName("Field")]
        public Guid? TransactionRuleFieldId { get; set; }

        public virtual EnumDto? TransactionRuleField { get; set; }

        [Required]
        [Display(Name = "Condition")]
        [JsonPropertyName("Condition")]
        public Guid? TransactionRuleConditionId { get; set; }

        public virtual EnumDto? TransactionRuleCondition { get; set; }

        [Required]
        public Guid? TransactionRulesGroupId { get; set; }

        public virtual CategoryDto? TransactionRulesGroup { get; set; }
    }
}
