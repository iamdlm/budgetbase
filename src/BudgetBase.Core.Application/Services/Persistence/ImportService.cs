using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Domain.Models;
using BudgetBase.Core.Domain.Entities;
using BudgetBase.Core.Application.Validators.Persistence;
using BudgetBase.Core.Application.Parsers;
using BudgetBase.Core.Application.Interfaces.Application;
using BudgetBase.Core.Application.Specifications;
using BudgetBase.Core.Domain.Interfaces;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public class ImportService : BaseService<Import, ImportDto>, IImportService
    {
        private ITransactionEntryTypeService _entryTypeService { get; }
        private ITransactionRulesGroupService _groupsService { get; }
        private ICurrentUserService _currentUserService { get; }

        public ImportService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ITransactionEntryTypeService entryTypeService,
            ITransactionRulesGroupService groupsService,
            ICurrentUserService currentUserService)
            : base(unitOfWork, mapper, currentUserService)
        {
            _entryTypeService = entryTypeService;
            _groupsService = groupsService;
            _currentUserService = currentUserService;
        }

        protected override Task ValidateCreateDtoAsync(ImportDto dto)
        {
            var validator = new ImportDtoValidator();
            var result = validator.Validate(dto);

            if (result?.IsValid != true)
            {
                throw new ValidationException(result.Errors);
            }

            return Task.CompletedTask;
        }

        protected override async Task ValidateUpdateDtoAsync(ImportDto dto)
        {
            await ValidateCreateDtoAsync(dto).ConfigureAwait(false);
        }

        public async Task ImportAsync(ImportDto importDto, List<TransactionDto> transactionsDto, Guid? sourceAccountId = null)
        {
            List<Transaction> transactions = new();
            List<TransactionImport> transactionImports = new();
            Import import = _mapper.Map<Import>(importDto);
            import.Id = Guid.NewGuid();

            ISpecification<Transaction> transactionSpec = new OwnedByUserSpecification<Transaction>(_currentUserService.UserId);
            ISpecification<TransactionRulesGroup> rulesGroupSpec = new OwnedByUserSpecification<TransactionRulesGroup>(_currentUserService.UserId);

            foreach (var transactionDto in transactionsDto)
            {
                Transaction transaction = _mapper.Map<Transaction>(transactionDto);
                transaction.SourceAccountId = sourceAccountId != null ? sourceAccountId.Value : Guid.Empty;
                transaction.TransactionEntryTypeId = _entryTypeService.GetByNameAsync(Constants.TransactionEntryTypes.Auto).Result.Id;

                if (!importDto.InsertDuplicates)
                {
                    Transaction existingTransaction = await _unitOfWork.TransactionRepo.FirstOrDefaultAsync(
                        t => t.Description == transaction.Description && t.Amount == transaction.Amount && t.Date == transaction.Date && t.SourceAccountId == transaction.SourceAccountId,
                        null,
                        transactionSpec,
                        null).ConfigureAwait(false);

                    if (existingTransaction != null)
                    {
                        continue;
                    }
                }

                if (!importDto.IgnoreRules)
                {
                    Transaction similarTransaction = await _unitOfWork.TransactionRepo.FirstOrDefaultAsync(
                        t => t.Description == transaction.Description,
                        null,
                        transactionSpec,
                        null).ConfigureAwait(false);

                    if (similarTransaction != null)
                    {
                        transaction.TransactionCategory = similarTransaction.TransactionCategory;
                        transaction.TransactionCategoryId = similarTransaction.TransactionCategoryId;
                    }

                    await _groupsService.ApplyRulesAsync(transaction, _unitOfWork.TransactionRulesGroupsRepo.GetAll(
                        null,
                        null,
                        rulesGroupSpec,
                        i => i.TransactionRules,
                        i => i.TransactionCategory,
                        i => i.TransactionRulesGroupOperator).ToList()).ConfigureAwait(false);
                }

                transactions.Add(transaction);

                transactionImports.Add(new TransactionImport
                {
                    Transaction = transaction,
                    Import = import,
                    ImportId = import.Id,
                });
            }

            _unitOfWork.ImportRepo.Add(import);
            _unitOfWork.TransactionRepo.AddRange(_mapper.Map<IEnumerable<Transaction>>(transactions));
            _unitOfWork.TransactionImportsRepo.AddRange(transactionImports);

            await _unitOfWork.CompleteAsync().ConfigureAwait(false);
        }

        public async Task DeleteImportAsync(Guid id)
        {
            Import import = await _unitOfWork.ImportRepo.GetByIdAsync(id).ConfigureAwait(false);

            if (import == null)
            {
                throw new ValidationException("Not found.");
            }

            IEnumerable<TransactionImport> transactionImports = _unitOfWork.TransactionImportsRepo.GetWhere(m => m.ImportId == id);
            IEnumerable<Transaction> transactions = _unitOfWork.TransactionRepo.GetWhere(m => transactionImports.Select(i => i.TransactionId).Contains(m.Id));

            _unitOfWork.TransactionImportsRepo.DeleteRange(transactionImports);
            _unitOfWork.ImportRepo.Delete(import);
            _unitOfWork.TransactionRepo.DeleteRange(transactions);

            await _unitOfWork.CompleteAsync().ConfigureAwait(false);
        }
    }
}
