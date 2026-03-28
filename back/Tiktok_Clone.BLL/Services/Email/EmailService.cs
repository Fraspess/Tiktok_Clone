using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Tiktok_Clone.BLL.Settings;

namespace Tiktok_Clone.BLL.Services.Email
{
    public class EmailService(IOptions<EmailSettings> settings) : IEmailService
    {
        private readonly EmailSettings _settings = settings.Value;
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_settings.FromName, _settings.Username));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_settings.Username, _settings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
