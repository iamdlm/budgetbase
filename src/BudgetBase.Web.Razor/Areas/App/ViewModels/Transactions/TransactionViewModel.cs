using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.Transactions
{
    public class TransactionViewModel : BaseEntityViewModel
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = default!;

        public decimal Amount { get; set; } = default!;

        public string Description { get; set; } = default!;

        [Required]
        [Display(Name = "Source Account")]
        [JsonPropertyName("Source Account")]
        public Guid? SourceAccountId { get; set; }

        public virtual AccountDto? SourceAccount { get; set; }

        [Display(Name = "Target Account")]
        [JsonPropertyName("Target Account")]
        public Guid? TargetAccountId { get; set; }

        public virtual AccountDto? TargetAccount { get; set; }

        [Required]
        [Display(Name = "Category")]
        [JsonPropertyName("Category")]
        public Guid? TransactionCategoryId { get; set; }

        public virtual CategoryDto? TransactionCategory { get; set; }

        [Display(Name = "Entry")]
        [JsonPropertyName("Entry")]
        public Guid? TransactionEntryTypeId { get; set; }

        public virtual EnumDto? TransactionEntryType { get; set; }

        [Display(Name = "Recurrency")]
        [JsonPropertyName("Recurrency")]
        public Guid? RecurrencyTypeId { get; set; }

        public virtual EnumDto? RecurrencyType { get; set; }
    }
}
