namespace BudgetBase.Core.Domain.Models
{
    public static class Constants
    {
        public static class TransactionTypes
        {
            public static readonly string Expense = "Expense";
            public static readonly string Income = "Income";
            public static readonly string Transfer = "Transfer";
        }

        public static class TransactionEntryTypes
        {
            public static readonly string Auto = "Auto";
            public static readonly string Manual = "Manual";
            public static readonly string TransactionRule = "TransactionRule";
        }

        public static class RecurrencyTypes
        {
            public static readonly string Daily = "Daily";
            public static readonly string Weekly = "Weekly";
            public static readonly string BiWeekly = "Bi-Weekly";
            public static readonly string Monthly = "Monthly";
            public static readonly string BiMonthly = "Bi-Monthly";
            public static readonly string Quarterly = "Quarterly";
            public static readonly string SemiAnnually = "Semi-annually";
            public static readonly string Annually = "Annually";
            public static readonly string Biennially = "Biennially";
        }

        public static class TransactionRuleFields
        {
            public static readonly string Amount = "Amount";
            public static readonly string Date = "Date";
            public static readonly string Description = "Description";
        }

        public static class TransactionRuleConditions
        {
            public static readonly string Equals = "Equals";
            public static readonly string NotEquals = "Not Equals";
            public static readonly string GreaterThan = "Greater Than";
            public static readonly string GreaterThanOrEquals = "Greater Than or Equals";
            public static readonly string LessThan = "Less Than";
            public static readonly string LessThanOrEquals = "Less Than or Equals";
            public static readonly string Contains = "Contains";
            public static readonly string NotContains = "Not Contains";
            public static readonly string StartsWith = "Starts With";
            public static readonly string NotStartsWith = "Not Starts With";
            public static readonly string EndsWith = "Ends With";
            public static readonly string NotEndsWith = "Not Ends With";
        }

        public static class TransactionRulesGroupOperators
        {
            public static readonly string And = "And";
            public static readonly string Or = "Or";
        }
    }
}
