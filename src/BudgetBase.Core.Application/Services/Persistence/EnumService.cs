using AutoMapper;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public abstract class EnumService<TEntity, TDto> : IEnumService<TDto>
        where TEntity : Domain.Entities.Enum
        where TDto : class
    {
        public IUnitOfWork _unitOfWork { get; }
        public IMapper _mapper { get; }

        public EnumService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<IEnumerable<TDto>> GetAllAsync()
        {
            IEnumerable<TEntity> entities = _unitOfWork.GetRepository<TEntity>().GetAll(null, o => o.OrderBy(m => m.Index));
            return Task.FromResult(_mapper.Map<IEnumerable<TDto>>(entities));
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

        public async Task<TDto> GetByNameAsync(string name)
        {
            var entity = await _unitOfWork.GetRepository<TEntity>().FirstOrDefaultAsync(m => m.Description.ToLower() == name.ToLower(), null, null).ConfigureAwait(false);

            if (entity == null)
            {
                throw new ValidationException("Not found.");
            }

            return _mapper.Map<TDto>(entity);
        }

        public async Task CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _unitOfWork.GetRepository<TEntity>().Add(entity);
            await _unitOfWork.CompleteAsync().ConfigureAwait(false);
        }
    }
}
