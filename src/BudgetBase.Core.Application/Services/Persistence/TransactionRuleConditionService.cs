using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Domain.Entities;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public class TransactionRuleConditionService : EnumService<TransactionRuleCondition, EnumDto>, ITransactionRuleConditionService
    {
        public IUnitOfWork _unitOfWork { get; }
        public IMapper _mapper { get; }

        public TransactionRuleConditionService(
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
