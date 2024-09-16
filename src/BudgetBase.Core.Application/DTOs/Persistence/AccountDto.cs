using BudgetBase.Core.Application.Interfaces.Persistence;
using System.ComponentModel.DataAnnotations;

namespace BudgetBase.Core.Application.DTOs.Persistence
{
    public class AccountDto : IIdentifiable
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string IBAN { get; set; }
    }
}
