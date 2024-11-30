using Shop.Domain.Entities;

namespace Shop.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}