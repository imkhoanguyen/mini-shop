using Microsoft.AspNetCore.Http;
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
        private readonly ISizeService _sizeService;
        private readonly IColorService _colorService;
        public VariantService(IUnitOfWork unit, ICloudinaryService cloudinaryService,
        IColorService colorService, ISizeService sizeService)
        {
            _unit = unit;
            _cloudinaryService = cloudinaryService;
            _colorService = colorService;
            _sizeService = sizeService;
        }
        public async Task<VariantDto> AddAsync(VariantAdd variantAdd)
        {
            var variant = VariantMapper.VariantAddDtoToEntity(variantAdd);
            if (variantAdd.ImageFiles?.Count > 0)
            {
                foreach (var file in variantAdd.ImageFiles)
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

        public async Task<VariantDto> AddImageAsync(int variantId, List<IFormFile> files)
        {
            var variant = await _unit.VariantRepository.GetAsync(r => r.Id == variantId, true);
            if (variant == null)
                throw new NotFoundException("variant not found");

            if (files.Count < 1)
                throw new BadRequestException("list image is empty");

            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    var result = await _cloudinaryService.UploadImageAsync(file);
                    if (result.Error != null)
                    {
                        throw new BadRequestException(result.Error);
                    }
                    var img = new VariantImage
                    {
                        ImgUrl = result.Url,
                        PublicId = result.PublicId,
                    };
                    variant.Images.Add(img);
                }
            }

            if (await _unit.CompleteAsync())
            {
                return VariantMapper.EntityToVariantDto(variant);
            }

            throw new BadRequestException("Problem with add image of variant");
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

            variant.Color = await _colorService.GetColorsById(variant.ColorId);
            variant.Size = await _sizeService.GetSizesById(variant.SizeId);
            
            return VariantMapper.EntityToVariantDto(variant);
        }

        public async Task<IEnumerable<VariantDto>> GetByProductId(int ProductId)
        {
            var variants = await _unit.VariantRepository.GetByProductIdAsync(ProductId);
            foreach (var variant in variants)
            {
                    variant.Color = await _colorService.GetColorsById(variant.ColorId);
                    variant.Size = await _sizeService.GetSizesById(variant.SizeId);

                
            }
            return variants.Select(VariantMapper.EntityToVariantDto);
        }

        public async Task RemoveImageAsync(int variantId, int imageId)
        {
            var variant = await _unit.VariantRepository.GetAsync(v => v.Id == variantId, true);
            if (variant == null)
                throw new NotFoundException("variant not found");

            var img = variant.Images.FirstOrDefault(i => i.Id == imageId);
            if (img == null)
                throw new NotFoundException("Image not found");
            // remove image on cloudinary
            if (img.PublicId != null)
            {
                var result = await _cloudinaryService.DeleteImageAsync(img.PublicId);
                if (result.Error != null)
                {
                    throw new BadRequestException(result.Error);
                }
            }

            //remove image on db
            variant.Images.Remove(img);

            if (!await _unit.CompleteAsync())
            {
                throw new BadRequestException("Problem remove image variant");
            }
        }

        public async Task<VariantDto> UpdateAsync(VariantUpdate variantUpdate)
        {
            if (!await _unit.VariantRepository.ExistsAsync(c => c.Id == variantUpdate.Id))
                throw new NotFoundException("Biến thể không tồn tại");

            var variant = VariantMapper.VariantUpdateDtoToEntity(variantUpdate);
            await _unit.VariantRepository.UpdateVariantAsync(variant);

            return await _unit.CompleteAsync()
                ? VariantMapper.EntityToVariantDto(variant)
                : throw new BadRequestException("Cập nhật Biến thể thất bại");
        }
    }
}
