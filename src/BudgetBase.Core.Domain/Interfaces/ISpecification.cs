using System.Linq.Expressions;

namespace BudgetBase.Core.Domain.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}
