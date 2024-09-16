using BudgetBase.Core.Application.DTOs.Persistence;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BudgetBase.Web.Razor.Areas.App.ViewModels.Categories
{
    public class CategoryViewModel : BaseEntityViewModel
    {
        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; }

        public string? Color { get; set; }

        [DisplayName("Type")]
        [JsonPropertyName("Type")]
        public Guid? TransactionTypeId { get; set; }

        public virtual EnumDto? TransactionType { get; set; }

        [DisplayName("Parent Category")]
        [JsonPropertyName("Category")]
        public Guid? ParentTransactionCategoryId { get; set; }

        public virtual CategoryDto? ParentTransactionCategory { get; set; }
    }
}
