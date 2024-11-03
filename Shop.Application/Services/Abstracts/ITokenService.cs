
using Shop.Domain.Entities;

namespace Shop.Application.Services.Abstracts
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}