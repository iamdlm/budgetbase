using AutoMapper;
using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Application;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Application.Specifications;
using BudgetBase.Core.Domain.Interfaces;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public abstract class BaseService<TEntity, TDto> : IBaseService<TDto>
        where TEntity : class
        where TDto : class, IIdentifiable
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly ICurrentUserService _currentUserService;

        public BaseService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.GetRepository<TEntity>().GetByIdAsync(id).ConfigureAwait(false);

            if (entity == null)
            {
                throw new ValidationException("Not found.");
            }

            return _mapper.Map<TDto>(entity);
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            IEnumerable<TEntity> entities = await _unitOfWork.GetRepository<TEntity>().GetAllAsync(GetUserOwnedSpecification()).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async Task CreateAsync(TDto dto)
        {
            await ValidateCreateDtoAsync(dto).ConfigureAwait(false);

            var entity = _mapper.Map<TEntity>(dto);
            _unitOfWork.GetRepository<TEntity>().Add(entity);
            await _unitOfWork.CompleteAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(TDto dto)
        {
            await ValidateUpdateDtoAsync(dto).ConfigureAwait(false);

            TEntity entity = await _unitOfWork.GetRepository<TEntity>().GetByIdAsync(dto.Id).ConfigureAwait(false);
            _mapper.Map(dto, entity);
            _unitOfWork.GetRepository<TEntity>().Update(entity);
            await _unitOfWork.CompleteAsync().ConfigureAwait(false);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            TEntity entity = await _unitOfWork.GetRepository<TEntity>().GetByIdAsync(id).ConfigureAwait(false);

            if (entity == null)
            {
                throw new ValidationException("Not found.");
            }

            _unitOfWork.GetRepository<TEntity>().Delete(entity);
            await _unitOfWork.CompleteAsync().ConfigureAwait(false);
        }

        public async Task<PaginatedResult<TDto>> GetPaginatedAsync(int pageNumber, int pageSize, string search, string sortProperty, string sortDirection, List<string> visibleColumns, List<string> includeProperties)
        {
            PaginatedResult<TEntity> result = await _unitOfWork.GetRepository<TEntity>().GetPaginatedAsync(
                pageNumber,
                pageSize,
                search,
                sortProperty,
                sortDirection,
                visibleColumns,
                GetUserOwnedSpecification(),
                includeProperties).ConfigureAwait(false);

            return new PaginatedResult<TDto>()
            {
                Items = _mapper.Map<IEnumerable<TDto>>(result.Items),
                LastPage = result.LastPage,
                LastRow = result.LastRow
            };
        }

        protected abstract Task ValidateCreateDtoAsync(TDto dto);

        protected abstract Task ValidateUpdateDtoAsync(TDto dto);

        protected ISpecification<TEntity> GetUserOwnedSpecification()
        {
            // Check if TEntity implements IBaseAuditableEntity
            if (typeof(IBaseAuditableEntity).IsAssignableFrom(typeof(TEntity)))
            {
                // Safe to create specification due to type check
                var userId = _currentUserService.UserId;
                var spec = typeof(OwnedByUserSpecification<>).MakeGenericType(typeof(TEntity));
                return (ISpecification<TEntity>)Activator.CreateInstance(spec, new object[] { userId });
            }

            // Return null or a default specification if TEntity does not implement IBaseAuditableEntity
            return null;
        }
    }
}
