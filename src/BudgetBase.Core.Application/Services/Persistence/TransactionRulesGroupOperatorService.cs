using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Domain.Entities;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public class TransactionRulesGroupOperatorService : EnumService<TransactionRulesGroupOperator, EnumDto>, ITransactionRulesGroupOperatorService
    {
        public IUnitOfWork _unitOfWork { get; }
        public IMapper _mapper { get; }

        public TransactionRulesGroupOperatorService(
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
