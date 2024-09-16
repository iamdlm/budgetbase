using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces;
using BudgetBase.Core.Application.Interfaces.Application;

namespace BudgetBase.Core.Application.Services.Application
{
    public class BankTransactionParserService : IBankTransactionParserService
    {
        private readonly IServiceProvider _serviceProvider;

        public BankTransactionParserService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IBankTransactionParser GetParser(string bankIdentifier)
        {
            // Resolve the parser based on bankIdentifier
            // Example: $"{bankIdentifier}Parser" assuming naming convention like "BankAParser"
            var parserTypeName = $"BudgetBase.Core.Application.Parsers.{bankIdentifier}TransactionParser";

            var parserType = Type.GetType(parserTypeName);
            if (parserType != null)
            {
                return (IBankTransactionParser)_serviceProvider.GetService(parserType);
            }

            throw new ValidationException("Import.BankId", "Unsupported bank identifier.");
        }
    }
}
