using FluentValidation.Results;

namespace BudgetBase.Core.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(string error)
            : this()
        {
            Errors = new Dictionary<string, string[]>() { { string.Empty, new string[] { error } } };
        }

        public ValidationException(string key, string error)
            : this()
        {
            Errors = new Dictionary<string, string[]>() { { key, new string[] { error } } };
        }

        public ValidationException(IEnumerable<ValidationFailure> failures, string className = null)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => className != null ? $"{className}.{failureGroup.Key}" : failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
