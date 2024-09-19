using BudgetBase.Core.Application.Interfaces.Common;
using BudgetBase.Core.Domain.Configurations;
using DocumentFormat.OpenXml.Spreadsheet;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text;
using System.Text.Encodings.Web;

namespace BudgetBase.Infrastructure.Common.Services
{
    public class EmailService : IEmailService
    {
        public SmtpOptions _smtpOptions { get; }
        public EmailConfirmationOptions _confirmOptions { get; }

        public EmailService(IOptions<SmtpOptions> options, IOptions<EmailConfirmationOptions> confirmOptions)
        {
            _smtpOptions = options.Value;
            _confirmOptions = confirmOptions.Value;
        }

        public async Task SendEmailConfirmationAsync(string userEmail, string userId, string code)
        {
            string subject = "Confirm your email";

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            string confirmationLink = $"{Environment.GetEnvironmentVariable(_confirmOptions.BaseUrl)}/{_confirmOptions.ConfirmationUrl}?code={code}&userId={userId}&returnUrl={_confirmOptions.CallbackUrl}";
            string message = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>clicking here</a>.";

            await SendEmailAsync(userEmail, subject, message).ConfigureAwait(false);
        }

        public async Task SendEmailConfirmationChangeAsync(string userEmail, string userId, string code)
        {
            string subject = "Confirm your email";

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            string confirmationLink = $"{Environment.GetEnvironmentVariable(_confirmOptions.BaseUrl)}/{_confirmOptions.ConfirmationChangeUrl}?code={code}&email={userEmail}&returnUrl={_confirmOptions.CallbackUrl}";
            string message = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>clicking here</a>.";

            await SendEmailAsync(userEmail, subject, message).ConfigureAwait(false);
        }

        public async Task SendPasswordResetAsync(string userEmail, string code)
        {
            string subject = "Reset your password";

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            string callbackUrl = $"{Environment.GetEnvironmentVariable(_confirmOptions.BaseUrl)}/{_confirmOptions.ResetPasswordUrl}?code={code}";
            string message = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

            await SendEmailAsync(userEmail, subject, message).ConfigureAwait(false);
        }

        private async Task SendEmailAsync(string recipient, string subject, string message)
        {
            MimeMessage email = new MimeMessage();
            email.From.Add(new MailboxAddress(_confirmOptions.Name, Environment.GetEnvironmentVariable(_smtpOptions.Email)));
            email.To.Add(MailboxAddress.Parse(recipient));
            email.Subject = subject;

            BodyBuilder bodyBuilder = new BodyBuilder { HtmlBody = message };
            email.Body = bodyBuilder.ToMessageBody();

            using SmtpClient smtp = new SmtpClient();

            // Try to parse the Port value
            if (!int.TryParse(Environment.GetEnvironmentVariable(_smtpOptions.Port), out int port))
            {
                // Handle the error or set a default port
                port = 25; // Default SMTP port
            }

            // Try to parse the SecureSocketOptions value
            SecureSocketOptions secureSocketOptions = SecureSocketOptions.Auto;
            if (int.TryParse(Environment.GetEnvironmentVariable(_smtpOptions.SecureSocketOptions), out int sso))
            {
                secureSocketOptions = (SecureSocketOptions)sso;
            }

            smtp.Connect(Environment.GetEnvironmentVariable(_smtpOptions.Host), port, secureSocketOptions);
            smtp.Authenticate(Environment.GetEnvironmentVariable(_smtpOptions.Email), Environment.GetEnvironmentVariable(_smtpOptions.Password));
            await smtp.SendAsync(email).ConfigureAwait(false);
            smtp.Disconnect(true);
        }
    }
}
