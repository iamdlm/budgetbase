using BudgetBase.Core.Domain.Entities;

namespace BudgetBase.Core.Application.Interfaces.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<Account> AccountRepo { get; }
        public IBaseRepository<Transaction> TransactionRepo { get; }
        public IBaseRepository<TransactionCategory> TransactionCategoryRepo { get; }
        public IBaseRepository<TransactionType> TransactionTypeRepo { get; }
        public IBaseRepository<TransactionEntryType> TransactionEntryTypeRepo { get; }
        public IBaseRepository<RecurrencyType> RecurrencyTypeRepo { get; }
        public IBaseRepository<Country> CountryRepo { get; }
        public IBaseRepository<Bank> BankRepo { get; }
        public IBaseRepository<Import> ImportRepo { get; }
        public IBaseRepository<TransactionImport> TransactionImportsRepo { get; }
        public IBaseRepository<TransactionRule> TransactionRulesRepo { get; }
        public IBaseRepository<TransactionRuleField> TransactionRuleFieldsRepo { get; }
        public IBaseRepository<TransactionRuleCondition> TransactionRuleConditionsRepo { get; }
        public IBaseRepository<TransactionRulesGroup> TransactionRulesGroupsRepo { get; }
        IBaseRepository<T> GetRepository<T>() where T : class;
        Task<bool> CompleteAsync();
    }
}
