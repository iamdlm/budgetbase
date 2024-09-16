using BudgetBase.Core.Application.Interfaces.Persistence;
using System.ComponentModel.DataAnnotations;

namespace BudgetBase.Core.Application.DTOs.Persistence
{
    public class ImportDto : IIdentifiable
    {
        public Guid Id { get; set; }

        public Guid CountryId { get; set; }

        public virtual CountryDto? Country { get; set; }

        public Guid BankId { get; set; }

        public virtual BankDto? Bank { get; set; }

        public Guid SourceAccountId { get; set; }

        public virtual AccountDto? SourceAccount { get; set; }

        public string Filename { get; set; }

        public bool InsertDuplicates { get; set; }

        public bool IgnoreRules { get; set; }

        public DateTime CreatedOn { get; set; }

        public int TransactionsCount { get; set; }

        public int TransactionsInserted { get; set; }
    }
}
