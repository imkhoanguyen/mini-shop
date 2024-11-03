using Shop.Application.Repositories;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface IImageRepository : IRepository<Image>
    {
        Task UpdateImage(Image image);
    }
}