using Shop.Application.DTOs.Auth;

namespace Shop.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendMailAsync(CancellationToken cancellationToken, EmailRequest emailRequest);
    }
}