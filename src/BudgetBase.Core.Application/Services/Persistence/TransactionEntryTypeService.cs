using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Domain.Entities;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public class TransactionEntryTypeService : EnumService<TransactionEntryType, EnumDto>, ITransactionEntryTypeService
    {
        public IUnitOfWork _unitOfWork { get; }
        public IMapper _mapper { get; }

        public TransactionEntryTypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
