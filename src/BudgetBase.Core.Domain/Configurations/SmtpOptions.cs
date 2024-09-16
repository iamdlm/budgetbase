namespace BudgetBase.Core.Domain.Configurations
{
    public class SmtpOptions
    {
        public const string Smtp = "Smtp";
        public string Email { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string SecureSocketOptions { get; set; }
    }
}
