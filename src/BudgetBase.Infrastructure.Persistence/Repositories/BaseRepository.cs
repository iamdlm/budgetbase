using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Domain.Interfaces;
using BudgetBase.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BudgetBase.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _appContext;

        public BaseRepository(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public void Add(T entity) => _appContext.Set<T>().Add(entity);

        public void AddRange(IEnumerable<T> entities) => _appContext.Set<T>().AddRange(entities);

        public void Update(T entity) => _appContext.Set<T>().Update(entity);

        public void Delete(T entity) => _appContext.Set<T>().Remove(entity);

        public void DeleteRange(IEnumerable<T> entities) => _appContext.Set<T>().RemoveRange(entities);

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, ISpecification<T> specification = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _appContext.Set<T>();

            if (includes != null)
            {
                foreach (Expression<Func<T, object>> include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (specification != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public IQueryable<T> GetAll(ISpecification<T> specification = null)
        {
            IQueryable<T> query = _appContext.Set<T>();

            if (specification != null)
            {
                query = query.Where(specification.Criteria);
            }

            return query;
        }

        public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification = null)
        {
            IQueryable<T> query = _appContext.Set<T>();

            if (specification != null)
            {
                query = query.Where(specification.Criteria);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, ISpecification<T> specification = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _appContext.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (specification != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> filter, ISpecification<T> specification = null)
        {
            IQueryable<T> query = _appContext.Set<T>();

            if (specification != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public async Task<T> GetByIdAsync(Guid id) => await _appContext.Set<T>().FindAsync(id);

        public async Task<int> CountAsync() => await _appContext.Set<T>().CountAsync().ConfigureAwait(false);

        public async Task<PaginatedResult<T>> GetPaginatedAsync(int page, int size, string search, string sortProperty, string sortDirection, List<string> visibleColumns, ISpecification<T> specification, List<string> includeProperties = null)
        {
            IQueryable<T> query = _appContext.Set<T>().Where(specification.Criteria);

            // Building Filters
            if (!string.IsNullOrEmpty(search))
            {
                // Build lambda expression to search in all properties of the entity
                var param = Expression.Parameter(typeof(T), "x");
                Expression body = null;

                foreach (var colName in visibleColumns)
                {
                    var parts = colName.Split('.');
                    var prop = typeof(T).GetProperty(parts[0]);
                    if (prop == null) continue;

                    for (int i = 1; i < parts.Length; i++)
                    {
                        prop = prop.PropertyType.GetProperty(parts[i]);
                        if (prop == null) break;
                        if (prop.PropertyType.IsGenericType) continue;
                    }

                    var member = GetPropertyExpression(param, colName);
                    if (member == null) continue;

                    var contains = Expression.Call(
                        Expression.Call(member, "ToLower", Type.EmptyTypes),
                        "Contains",
                        Type.EmptyTypes,
                        Expression.Constant(search.ToLower())
                    );

                    if (body == null)
                    {
                        body = contains;
                    }
                    else
                    {
                        body = Expression.Or(body, contains);
                    }
                }

                var filter = Expression.Lambda<Func<T, bool>>(body, param);

                // Apply filters
                query = query.Where(filter);
            }

            // Count the total number of items in the query
            var totalCount = await query.CountAsync().ConfigureAwait(false);

            // Include related entities
            if (includeProperties != null)
            {
                foreach (string includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            // Building Sorting
            if (!string.IsNullOrEmpty(sortProperty))
            {
                var dir = sortDirection == "asc";

                var parts = sortProperty.Split('.');
                var prop = typeof(T).GetProperty(parts[0]);

                for (int i = 1; i < parts.Length; i++)
                {
                    prop = prop.PropertyType.GetProperty(parts[i]);
                    if (prop == null) break;
                    if (prop.PropertyType.IsGenericType) continue;
                }

                var param = Expression.Parameter(typeof(T), "x");
                var member = GetPropertyExpression(param, sortProperty);
                var castMember = Expression.Convert(member, typeof(object)); // explicit cast to object
                var lambda = Expression.Lambda<Func<T, object>>(castMember, param);

                var orderedQuery = query.OrderBy(x => true);

                // Apply sorts
                query = sortDirection == "asc" ? orderedQuery.ThenBy(lambda) : orderedQuery.ThenByDescending(lambda);
            }

            // Calculate the number of items to skip
            var skipCount = (page - 1) * size;

            // Get the current page of items
            var currentPage = await query.Skip(skipCount).Take(size).ToListAsync().ConfigureAwait(false);

            // Calculate the total number of pages
            var pageCount = (int)Math.Ceiling((double)totalCount / size);

            return new PaginatedResult<T>
            {
                Items = currentPage,
                LastPage = pageCount,
                LastRow = totalCount
            };
        }

        private Expression GetPropertyExpression(Expression param, string columnName)
        {
            string[] parts = columnName.Split('.');

            if (parts.Length == 1)
            {
                Expression member = Expression.Property(param, columnName);

                if (member.Type != typeof(string))
                {
                    member = Expression.Call(member, "ToString", Type.EmptyTypes);
                }

                return member;
            }
            else
            {
                string propName = parts[0];
                var prop = typeof(T).GetProperty(propName);

                if (prop == null)
                {
                    return null;
                }

                Expression member = Expression.Property(param, prop.Name);

                return GetPropertyExpression(member, string.Join(".", parts.Skip(1)));
            }
        }
    }
}
