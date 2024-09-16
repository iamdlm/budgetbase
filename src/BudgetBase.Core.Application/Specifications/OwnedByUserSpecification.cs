using BudgetBase.Core.Domain.Interfaces;
using System.Linq.Expressions;

namespace BudgetBase.Core.Application.Specifications
{
    public class OwnedByUserSpecification<T> : ISpecification<T> where T : IBaseAuditableEntity
    {
        public string UserId { get; }

        public OwnedByUserSpecification(string userId)
        {
            UserId = userId;
        }

        public Expression<Func<T, bool>> Criteria => entity => entity.CreatedBy == UserId;
    }
}
