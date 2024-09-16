using BudgetBase.Core.Domain.Entities;
using BudgetBase.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace BudgetBase.Infrastructure.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public DbSet<TransactionCategory> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<TransactionEntryType> TransactionEntryTypes { get; set; }
        public DbSet<RecurrencyType> RecurrencyTypes { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Import> Imports { get; set; }
        public DbSet<TransactionImport> TransactionImports { get; set; }
        public DbSet<TransactionRule> TransactionRules { get; set; }
        public DbSet<TransactionRuleField> TransactionRuleFields { get; set; }
        public DbSet<TransactionRuleCondition> TransactionRuleConditions { get; set; }
        public DbSet<TransactionRulesGroup> TransactionRulesGroups { get; set; }

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
            : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TransactionImport>()
                .HasKey(ti => new { ti.TransactionId, ti.ImportId });

            modelBuilder.Entity<TransactionImport>()
                .HasOne(ti => ti.Transaction)
                .WithMany(t => t.TransactionImports)
                .HasForeignKey(ti => ti.TransactionId);

            modelBuilder.Entity<TransactionImport>()
                .HasOne(ti => ti.Import)
                .WithMany(i => i.TransactionImports)
                .HasForeignKey(ti => ti.ImportId);
        }
    }
}
