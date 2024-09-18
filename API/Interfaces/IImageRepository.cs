using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IImageRepository
    {
        Task<IEnumerable<ImageApprovalDto>> GetUnapprovedImagesAsync();
        Task<Image?> GetImageById(int id);
        void RemoveImage(Image image);
    }
}