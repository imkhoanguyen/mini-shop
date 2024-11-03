using API.Configurations;
using Microsoft.Extensions.Options;
using Shop.Application.DTOs.Auth;
using Shop.Application.Services.Abstracts;
using System.Net;
using System.Net.Mail;

namespace Shop.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        EmailConfig _emailConfig;
        public EmailService(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task SendMailAsync(CancellationToken cancellationToken, EmailRequest emailRequest)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(_emailConfig.Provider, _emailConfig.Port);
                smtpClient.Credentials = new NetworkCredential(_emailConfig.DefaultSender, _emailConfig.Password);
                smtpClient.EnableSsl = true;

                MailMessage message = new MailMessage();
                message.From = new MailAddress(_emailConfig.DefaultSender);
                message.To.Add(emailRequest.To);
                message.Subject = emailRequest.Subject;
                message.Body = emailRequest.Content;
                message.IsBodyHtml = true;

                await smtpClient.SendMailAsync(message, cancellationToken);
                message.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
    }
}
