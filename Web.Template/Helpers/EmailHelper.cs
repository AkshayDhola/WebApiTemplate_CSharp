using System.Net;
using System.Net.Mail;

namespace Web.Template.Helpers
{
    public static class EmailHelper
    {
        public record EmailSettings(
            string SmtpHost,
            int SmtpPort,
            string SenderEmail,
            string SenderPassword
        );

        private static  EmailSettings? _settings;

        public static void Configure(IConfiguration configuration)
        {
            _settings = configuration.GetSection("EmailSettings").Get<EmailSettings>()!;
        }

        public static async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            if (_settings is null)
            {
                throw new InvalidOperationException("EmailHelper not configured. Call Configure() first.");
            }

            var message = new MailMessage
            {
                From = new MailAddress(_settings.SenderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(toEmail);

            using var smtpClient = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort)
            {
                Credentials = new NetworkCredential(_settings.SenderEmail, _settings.SenderPassword),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(message);
        }
    }
}
