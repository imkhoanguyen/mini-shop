using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly StoreContext _context;
        public ImageRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Image?> GetImageById(int id)
        {
            return await _context.Images.IgnoreQueryFilters().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ImageApprovalDto>> GetUnapprovedImagesAsync()
        {
            return await _context.Images.IgnoreQueryFilters().Where(x => x.IsApproved == false)
                .Select(x => new ImageApprovalDto
                {
                    Id = x.Id,
                    Url = x.Url,
                    UserName = x.AppUser!.UserName,
                    ProductId = x.ProductId,
                    IsApproved = x.IsApproved
                }).ToListAsync();
        }

        public void RemoveImage(Image image)
        {
            _context.Images.Remove(image);
        }
    }
}