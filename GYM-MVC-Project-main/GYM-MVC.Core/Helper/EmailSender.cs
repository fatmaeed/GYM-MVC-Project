using GYM_MVC.Core.Helper;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

public class EmailSender : IEmailSender {
    private readonly EmailConfiguration _emailConfig;

    public EmailSender(IOptions<EmailConfiguration> emailConfig) {
        _emailConfig = emailConfig.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage) {
        using (var client = new SmtpClient(_emailConfig.SmtpServer, _emailConfig.SmtpPort)) {
            client.EnableSsl = _emailConfig.EnableSsl;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(
                _emailConfig.SmtpUsername,
                _emailConfig.SmtpPassword);

            var mailMessage = new MailMessage {
                From = new MailAddress(_emailConfig.FromAddress, _emailConfig.FromName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}