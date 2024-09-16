using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Domain.Entities;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public class TransactionTypeService : EnumService<TransactionType, EnumDto>, ITransactionTypeService
    {
        public IUnitOfWork _unitOfWork { get; }
        public IMapper _mapper { get; }

        public TransactionTypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
