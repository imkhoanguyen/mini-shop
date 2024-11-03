using API.Interfaces;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        private readonly StoreContext _context;
        public ImageRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public Task UpdateImage(Image image)
        {
            throw new NotImplementedException();
        }



        //public void AddImage(Image image)
        //{
        //    var existingImage = _context.Images.FirstOrDefault(i => i.VariantId == image.VariantId);
        //    _context.Images.Add(image);
        //}
        //public void UpdateImage(Image image)
        //{
        //    var existingImage = _context.Images
        //    .FirstOrDefault(i => i.Id == image.Id);
        //    if (existingImage is not null)
        //    {
        //        existingImage.Url = image.Url;
        //        existingImage.PublicId = image.PublicId;
        //        existingImage.IsMain = image.IsMain;
        //    }
        //}

        //public async Task<Image?> GetImageById(int id)
        //{
        //    return await _context.Images.IgnoreQueryFilters().SingleOrDefaultAsync(x => x.Id == id);
        //}

        //public async Task<IEnumerable<ImageApprovalDto>> GetUnapprovedImagesAsync()
        //{
        //    return await _context.Images.IgnoreQueryFilters().Where(x => x.IsApproved == false)
        //        .Select(x => new ImageApprovalDto
        //        {
        //            Id = x.Id,
        //            Url = x.Url,
        //            UserName = x.AppUser!.UserName,
        //            VariantId = x.VariantId,
        //            IsApproved = x.IsApproved
        //        }).ToListAsync();
        //}

        //public void RemoveImage(Image image)
        //{
        //    _context.Images.Remove(image);
        //}

        //Task IImageRepository.UpdateImage(Image image)
        //{
        //    throw new NotImplementedException();
        //}
    }
}