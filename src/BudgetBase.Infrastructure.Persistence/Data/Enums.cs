using BudgetBase.Core.Domain.Entities;
using BudgetBase.Core.Domain.Models;

namespace BudgetBase.Infrastructure.Persistence.Data
{
    public static class Enums
    {
        public static readonly TransactionType[] Types =
        {
            new() { Id = new Guid("993f38fb-e8d3-4c22-aa7f-8ada4ac40fd1"), Index = 0, Description = Constants.TransactionTypes.Expense },
            new() { Id = new Guid("993f38fb-e8d3-4c22-aa7f-8ada4ac40fd2"), Index = 1, Description = Constants.TransactionTypes.Income },
            new() { Id = new Guid("993f38fb-e8d3-4c22-aa7f-8ada4ac40fd3"), Index = 2, Description = Constants.TransactionTypes.Transfer }
        };

        public static readonly TransactionEntryType[] EntryTypes =
        {
            new() { Id = new Guid("123f38fb-e8d3-4c22-aa7f-8ada4ac40fd1"), Index = 0, Description = Constants.TransactionEntryTypes.Auto },
            new() { Id = new Guid("123f38fb-e8d3-4c22-aa7f-8ada4ac40fd2"), Index = 1, Description = Constants.TransactionEntryTypes.Manual },
            new() { Id = new Guid("123f38fb-e8d3-4c22-aa7f-8ada4ac40fd3"), Index = 2, Description = Constants.TransactionEntryTypes.TransactionRule }
        };

        public static readonly RecurrencyType[] RecurrencyTypes =
        {
            new() { Id = new Guid("71a4a638-cb8a-4061-92a3-fd35035eac51"), Index = 0, Description = Constants.RecurrencyTypes.Daily },
            new() { Id = new Guid("71a4a638-cb8a-4061-92a3-fd35035eac52"), Index = 1, Description = Constants.RecurrencyTypes.Weekly },
            new() { Id = new Guid("71a4a638-cb8a-4061-92a3-fd35035eac53"), Index = 2, Description = Constants.RecurrencyTypes.BiWeekly },
            new() { Id = new Guid("71a4a638-cb8a-4061-92a3-fd35035eac54"), Index = 3, Description = Constants.RecurrencyTypes.Monthly },
            new() { Id = new Guid("71a4a638-cb8a-4061-92a3-fd35035eac55"), Index = 4, Description = Constants.RecurrencyTypes.BiMonthly },
            new() { Id = new Guid("71a4a638-cb8a-4061-92a3-fd35035eac56"), Index = 5, Description = Constants.RecurrencyTypes.Quarterly },
            new() { Id = new Guid("71a4a638-cb8a-4061-92a3-fd35035eac57"), Index = 6, Description = Constants.RecurrencyTypes.SemiAnnually },
            new() { Id = new Guid("71a4a638-cb8a-4061-92a3-fd35035eac58"), Index = 7, Description = Constants.RecurrencyTypes.Annually },
            new() { Id = new Guid("71a4a638-cb8a-4061-92a3-fd35035eac59"), Index = 8, Description = Constants.RecurrencyTypes.Biennially }
        };

        public static readonly TransactionRuleField[] TransactionRuleFields =
        {
            new() { Id = new Guid("123f38fb-e8d1-4c21-aa7f-5ada4ac40fd1"), Index = 0, Description = Constants.TransactionRuleFields.Amount },
            new() { Id = new Guid("123f38fb-e8d2-4c22-aa7f-5ada4ac40fd2"), Index = 1, Description = Constants.TransactionRuleFields.Date },
            new() { Id = new Guid("123f38fb-e8d3-4c23-aa7f-5ada4ac40fd3"), Index = 2, Description = Constants.TransactionRuleFields.Description }
        };

        public static readonly TransactionRuleCondition[] TransactionRuleConditions =
        {
            new() { Id = new Guid("123f38fa-e8d1-8c22-aa7f-6ada4ac40fd0"), Index = 1, Description = Constants.TransactionRuleConditions.Equals },
            new() { Id = new Guid("123f38fb-e8d2-8c22-aa7f-6ada4ac40fd1"), Index = 2, Description = Constants.TransactionRuleConditions.NotEquals },
            new() { Id = new Guid("123f38fc-e8d3-7c23-aa7f-6ada4ac40fd2"), Index = 3, Description = Constants.TransactionRuleConditions.GreaterThan },
            new() { Id = new Guid("123f38fd-e8d4-7c23-aa7f-6ada4ac40fd3"), Index = 4, Description = Constants.TransactionRuleConditions.GreaterThanOrEquals },
            new() { Id = new Guid("123f38fe-e8d5-7c23-aa7f-6ada4ac40fd4"), Index = 5, Description = Constants.TransactionRuleConditions.LessThan },
            new() { Id = new Guid("123f38ff-e8d6-7c23-aa7f-6ada4ac40fd5"), Index = 6, Description = Constants.TransactionRuleConditions.LessThanOrEquals },
            new() { Id = new Guid("123f38fa-e8d7-9c21-aa7f-6ada4ac40fd6"), Index = 7, Description = Constants.TransactionRuleConditions.Contains },
            new() { Id = new Guid("123f38fb-e8d8-9c21-aa7f-6ada4ac40fd7"), Index = 8, Description = Constants.TransactionRuleConditions.NotContains },
            new() { Id = new Guid("123f38fc-e8d9-9c21-aa7f-6ada4ac40fd8"), Index = 9, Description = Constants.TransactionRuleConditions.StartsWith },
            new() { Id = new Guid("123f38fd-a8d1-9c21-aa7f-6ada4ac40fd9"), Index = 10, Description = Constants.TransactionRuleConditions.NotStartsWith },
            new() { Id = new Guid("123f38fe-b8d2-9c21-aa7f-6ada4ac41fd1"), Index = 11, Description = Constants.TransactionRuleConditions.EndsWith },
            new() { Id = new Guid("123f38ff-c8d3-9c21-aa7f-6ada4ac42fd2"), Index = 12, Description = Constants.TransactionRuleConditions.NotEndsWith },
        };

        public static readonly TransactionRulesGroupOperator[] TransactionRulesGroupOperators =
        {
            new() { Id = new Guid("123f38fa-e8d1-4c22-aa7f-8ada4ac40fd0"), Index = 0, Description = Constants.TransactionRulesGroupOperators.And },
            new() { Id = new Guid("123f38fb-e8d2-4c22-aa7f-8ada4ac40fd1"), Index = 1, Description = Constants.TransactionRulesGroupOperators.Or }
        };
    }
}