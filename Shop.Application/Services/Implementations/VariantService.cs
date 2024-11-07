using Shop.Application.DTOs.Variants;
using Shop.Application.Mappers;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using System.Linq.Expressions;

namespace Shop.Application.Services.Implementations
{
    public class VariantService : IVariantService
    {
        private readonly IUnitOfWork _unit;
        private readonly ICloudinaryService _cloudinaryService;
        public VariantService(IUnitOfWork unit, ICloudinaryService cloudinaryService)
        {
            _unit = unit;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<VariantDto> AddAsync(VariantAdd variantAdd)
        {
            var variant = VariantMapper.VariantAddDtoToEntity(variantAdd);
            if (variantAdd.ImageFile?.Count > 0)
            {
                foreach (var file in variantAdd.ImageFile)
                {
                    var uploadResult = await _cloudinaryService.UploadImageAsync(file);
                    if (uploadResult.Error != null)
                    {
                        throw new BadRequestException("Lỗi khi thêm ảnh");
                    }
                    var image = new VariantImage
                    {
                        ImgUrl = uploadResult.Url,
                        PublicId = uploadResult.PublicId,
                    };
                    variant.Images.Add(image);
                }
            }
            await _unit.VariantRepository.AddAsync(variant);

            return await _unit.CompleteAsync()
                ? VariantMapper.EntityToVariantDto(variant)
                : throw new BadRequestException("Thêm biến thể thất bại");
        }

        public async Task DeleteAsync(Expression<Func<Variant, bool>> expression)
        {
            var variant = await _unit.VariantRepository.GetAsync(expression);
            if (variant is null)
            {
                throw new NotFoundException("Biến thể không tồn tại");
            }
            await _unit.VariantRepository.DeleteVariantAsync(variant);
            if (!await _unit.CompleteAsync())
            {
                throw new BadRequestException("Xóa biến thể thất bại");
            }
        }

        public async Task<VariantDto?> GetAsync(Expression<Func<Variant, bool>> expression)
        {
            var variant = await _unit.VariantRepository.GetAsync(expression);
            if (variant is null) throw new NotFoundException("Biến thể không tồn tại");

            return VariantMapper.EntityToVariantDto(variant);
        }

        public async Task<IEnumerable<VariantDto>> GetByProductId(int ProductId)
        {
            var variants = await _unit.VariantRepository.GetByProductIdAsync(ProductId);
            return variants.Select(VariantMapper.EntityToVariantDto);
        }

        public async Task<VariantDto> UpdateAsync(VariantUpdate variantUpdate)
        {
            if (!await _unit.VariantRepository.ExistsAsync(c => c.Id == variantUpdate.Id))
                throw new NotFoundException("Biến thể không tồn tại");

            var variant = VariantMapper.VariantUpdateDtoToEntity(variantUpdate);
            if (variantUpdate.ImageFile?.Count > 0)
            {
                foreach (var file in variantUpdate.ImageFile)
                {
                    var uploadResult = await _cloudinaryService.UploadImageAsync(file);
                    if (uploadResult.Error != null)
                    {
                        throw new BadRequestException("Lỗi khi thêm ảnh");
                    }
                    var image = new VariantImage
                    {
                        ImgUrl = uploadResult.Url,
                        PublicId = uploadResult.PublicId,
                    };
                    variant.Images.Add(image);
                }
            }
            await _unit.VariantRepository.UpdateVariantAsync(variant);

            return await _unit.CompleteAsync()
                ? VariantMapper.EntityToVariantDto(variant)
                : throw new BadRequestException("Cập nhật Biến thể thất bại");
        }
    }
}
