using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IImageRepository
    {
        void AddImage(Image image);
        void UpdateImage(Image image);
        Task<IEnumerable<ImageApprovalDto>> GetUnapprovedImagesAsync();
        Task<Image?> GetImageById(int id);
        void RemoveImage(Image image);
    }
}