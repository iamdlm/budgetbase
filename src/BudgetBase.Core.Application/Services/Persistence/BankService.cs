using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Domain.Entities;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public class BankService : IBankService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BankService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BankDto> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.GetRepository<Bank>().GetByIdAsync(id).ConfigureAwait(false);

            if (entity == null)
            {
                throw new ValidationException("Not found.");
            }

            return _mapper.Map<BankDto>(entity);
        }

        public async Task<IEnumerable<BankDto>> GetAllAsync()
        {
            IEnumerable<Bank> entities = await _unitOfWork.GetRepository<Bank>().GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<BankDto>>(entities.OrderBy(x => x.Name));
        }

        public IEnumerable<BankDto> GetByCountryId(Guid countryId)
        {
            IEnumerable<Bank> entities = _unitOfWork.GetRepository<Bank>().GetWhere(x => x.CountryId == countryId);
            return _mapper.Map<IEnumerable<BankDto>>(entities.OrderBy(x => x.Name));
        }
    }
}
