using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Domain.Entities;
using BudgetBase.Infrastructure.Persistence.Data;

namespace BudgetBase.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IBaseRepository<Account> accountRepo;
        private readonly IBaseRepository<TransactionCategory> categoryRepo;
        private readonly IBaseRepository<Transaction> transactionRepo;
        private readonly IBaseRepository<TransactionType> typeRepo;
        private readonly IBaseRepository<TransactionEntryType> entryTypeRepo;
        private readonly IBaseRepository<RecurrencyType> recurrencyTypeRepo;
        private readonly IBaseRepository<Country> countryRepo;
        private readonly IBaseRepository<Bank> bankRepo;
        private readonly IBaseRepository<Import> importRepo;
        private readonly IBaseRepository<TransactionImport> transactionImportsRepo;
        private readonly IBaseRepository<TransactionRule> transactionRulesRepo;
        private readonly IBaseRepository<TransactionRuleField> transactionRuleFieldsRepo;
        private readonly IBaseRepository<TransactionRuleCondition> transactionRuleConditionsRepo;
        private readonly IBaseRepository<TransactionRulesGroup> transactionRulesGroupsRepo;
        private readonly Dictionary<Type, object> repositories;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            repositories = new Dictionary<Type, object>();
        }

        public IBaseRepository<Account> AccountRepo => accountRepo ?? new BaseRepository<Account>(_context);
        public IBaseRepository<Transaction> TransactionRepo => transactionRepo ?? new BaseRepository<Transaction>(_context);
        public IBaseRepository<TransactionCategory> TransactionCategoryRepo => categoryRepo ?? new BaseRepository<TransactionCategory>(_context);
        public IBaseRepository<TransactionType> TransactionTypeRepo => typeRepo ?? new BaseRepository<TransactionType>(_context);
        public IBaseRepository<TransactionEntryType> TransactionEntryTypeRepo => entryTypeRepo ?? new BaseRepository<TransactionEntryType>(_context);
        public IBaseRepository<RecurrencyType> RecurrencyTypeRepo => recurrencyTypeRepo ?? new BaseRepository<RecurrencyType>(_context);
        public IBaseRepository<Country> CountryRepo => countryRepo ?? new BaseRepository<Country>(_context);
        public IBaseRepository<Bank> BankRepo => bankRepo ?? new BaseRepository<Bank>(_context);
        public IBaseRepository<Import> ImportRepo => importRepo ?? new BaseRepository<Import>(_context);
        public IBaseRepository<TransactionImport> TransactionImportsRepo => transactionImportsRepo ?? new BaseRepository<TransactionImport>(_context);
        public IBaseRepository<TransactionRule> TransactionRulesRepo => transactionRulesRepo ?? new BaseRepository<TransactionRule>(_context);
        public IBaseRepository<TransactionRuleField> TransactionRuleFieldsRepo => transactionRuleFieldsRepo ?? new BaseRepository<TransactionRuleField>(_context);
        public IBaseRepository<TransactionRuleCondition> TransactionRuleConditionsRepo => transactionRuleConditionsRepo ?? new BaseRepository<TransactionRuleCondition>(_context);
        public IBaseRepository<TransactionRulesGroup> TransactionRulesGroupsRepo => transactionRulesGroupsRepo ?? new BaseRepository<TransactionRulesGroup>(_context);

        public IBaseRepository<T> GetRepository<T>() where T : class
        {
            // Check if the repository already exists in the dictionary.
            if (repositories.ContainsKey(typeof(T)))
            {
                return repositories[typeof(T)] as IBaseRepository<T>;
            }

            // If the repository doesn't exist, create it and add to the dictionary.
            IBaseRepository<T> repo = new BaseRepository<T>(_context);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public async Task<bool> CompleteAsync() => await _context.SaveChangesAsync().ConfigureAwait(false) > 0;

        public void Dispose() => _context.Dispose();
    }
}
