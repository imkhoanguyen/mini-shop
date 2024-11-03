using Shop.Application.DTOs.Auth;

namespace Shop.Application.Services.Abstracts
{
    public interface IEmailService
    {
        Task SendMailAsync(CancellationToken cancellationToken, EmailRequest emailRequest);
    }
}