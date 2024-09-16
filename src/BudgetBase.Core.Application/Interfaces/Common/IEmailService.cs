namespace BudgetBase.Core.Application.Interfaces.Common
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string userEmail, string userId, string code);
        Task SendEmailConfirmationChangeAsync(string userEmail, string userId, string code);
        Task SendPasswordResetAsync(string userEmail, string code);
    }
}