namespace BudgetBase.Core.Application.Interfaces.Application
{
    public interface IBankTransactionParserService
    {
        IBankTransactionParser GetParser(string bankIdentifier);
    }
}
