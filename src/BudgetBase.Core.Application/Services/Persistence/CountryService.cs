using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Domain.Entities;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CountryDto> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.GetRepository<Country>().GetByIdAsync(id).ConfigureAwait(false);

            if (entity == null)
            {
                throw new ValidationException("Not found.");
            }

            return _mapper.Map<CountryDto>(entity);
        }

        public async Task<IEnumerable<CountryDto>> GetAllAsync()
        {
            IEnumerable<Country> countries = await _unitOfWork.GetRepository<Country>().GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<CountryDto>>(countries.OrderBy(x => x.Name));
        }

        public async Task<IEnumerable<CountryDto>> GetAllWithAssociatedBankAsync()
        {
            IEnumerable<Bank> banks = await _unitOfWork.GetRepository<Bank>().GetAllAsync().ConfigureAwait(false);
            IEnumerable<Country> countries = _unitOfWork.GetRepository<Country>().GetWhere(c => banks.Select(b => b.CountryId).Contains(c.Id));
            return _mapper.Map<IEnumerable<CountryDto>>(countries.OrderBy(x => x.Name));
        }
    }
}
