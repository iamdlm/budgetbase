namespace BudgetBase.Core.Domain.Configurations
{
    public class EmailConfirmationOptions
    {
        public const string EmailConfirmation = "EmailConfirmation";
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public string ConfirmationUrl { get; set; }
        public string ConfirmationChangeUrl { get; set; }
        public string CallbackUrl { get; set; }
        public string ResetPasswordUrl { get; set; }
    }
}
