using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Domain.Interfaces;
using System.Linq.Expressions;

namespace BudgetBase.Core.Application.Interfaces.Persistence
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, ISpecification<T> specification = null, params Expression<Func<T, object>>[] includes);

        IQueryable<T> GetAll(ISpecification<T> specification = null);

        Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification = null);

        IQueryable<T> GetAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, ISpecification<T> specification = null, params Expression<Func<T, object>>[] includes);

        IQueryable<T> GetWhere(Expression<Func<T, bool>> filter, ISpecification<T> specification = null);

        Task<T> GetByIdAsync(Guid id);

        Task<PaginatedResult<T>> GetPaginatedAsync(int page, int size, string search, string sortProperty, string sortDirection, List<string> visibleColumns, ISpecification<T> specification = null, List<string> includeProperties = null);

        Task<int> CountAsync();
    }
}
