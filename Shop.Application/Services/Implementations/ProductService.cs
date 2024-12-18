﻿using API.Helpers;
using Microsoft.AspNetCore.Http;
using Shop.Application.DTOs.Orders;
using Shop.Application.DTOs.Products;
using Shop.Application.DTOs.Variants;
using Shop.Application.Interfaces;
using Shop.Application.Mappers;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using System.Linq.Expressions;

namespace Shop.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unit;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ICategoryService _categoryService;
        private readonly ISizeService _sizeService;
        private readonly IColorService _colorService;
        public ProductService(IUnitOfWork unit, ICloudinaryService cloudinaryService, ICategoryService categoryService,
            IColorService colorService, ISizeService sizeService)
        {
            _unit = unit;
            _cloudinaryService = cloudinaryService;
            _categoryService = categoryService;
            _colorService = colorService;
            _sizeService = sizeService;
        }
        public async Task<ProductDto> AddAsync(ProductAdd productAdd)
        {
            if (await _unit.ProductRepository.ExistsAsync(c => c.Name.ToLower() == productAdd.Name.ToLower() && !c.IsDelete))
            {
                throw new BadRequestException("Sản phẩm đã tồn tại");
            }
            var product = ProductMapper.ProductAddDtoToEntity(productAdd);
            if (productAdd.ImageFile != null)
            {
                var uploadResult = await _cloudinaryService.UploadImageAsync(productAdd.ImageFile);
                if (uploadResult.Error != null)
                {
                    throw new BadRequestException("Lỗi khi thêm ảnh");
                }
                var image = new ProductImage
                {
                    ImgUrl = uploadResult.Url,
                    PublicId = uploadResult.PublicId,
                };
                product.Image = image;

            }
            await _unit.ProductRepository.AddAsync(product);

            return await _unit.CompleteAsync()
                ? ProductMapper.EntityToProductDto(product)
                : throw new BadRequestException("Thêm sản phẩm thất bại");
        }

        public async Task<ProductDto> AddImageAsync(int productId, IFormFile file)
        {
            var product = await _unit.ProductRepository.GetAsync(r => r.Id == productId, true);
            if (product == null)
                throw new NotFoundException("product not found");

            if (file == null)
                throw new BadRequestException(" image is empty");

            var uploadResult = await _cloudinaryService.UploadImageAsync(file);
            if (uploadResult.Error != null)
            {
                throw new BadRequestException("Lỗi khi thêm ảnh");
            }
            var image = new ProductImage
            {
                ImgUrl = uploadResult.Url,
                PublicId = uploadResult.PublicId,
            };
            product.Image = image;


            if (await _unit.CompleteAsync())
            {
                return ProductMapper.EntityToProductDto(product);
            }

            throw new BadRequestException("Problem with add image of product");
        }

        public async Task DeleteAsync(Expression<Func<Product, bool>> expression)
        {
            var product = await _unit.ProductRepository.GetAsync(expression);
            if (product is null)
            {
                throw new NotFoundException("Sản phẩm không tồn tại");
            }
            await _unit.ProductRepository.DeleteProductAsync(product);
            if (!await _unit.CompleteAsync())
            {
                throw new BadRequestException("Xóa sản phẩm thất bại");
            }
        }

        public async Task<PagedList<ProductDto>> GetAllAsync(ProductParams productParams, bool tracked)
        {
            var products = await _unit.ProductRepository.GetAllProductsAsync(productParams, tracked);
            foreach (var product in products)
            {
                foreach (var variant in product.Variants)
                {
                    variant.Color = await _colorService.GetColorsById(variant.ColorId);
                    variant.Size = await _sizeService.GetSizesById(variant.SizeId);
                }
            }
            var productDto = products.Select(ProductMapper.EntityToProductDto);
            return new PagedList<ProductDto>(productDto, products.TotalCount, productParams.PageNumber, productParams.PageSize);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(bool tracked)
        {
            var products = await _unit.ProductRepository.GetAllProductsAsync(tracked);
            foreach (var product in products)
            {
                foreach (var variant in product.Variants)
                {
                    variant.Color = await _colorService.GetColorsById(variant.ColorId);
                    variant.Size = await _sizeService.GetSizesById(variant.SizeId);
                }
            }
            return products.Select(ProductMapper.EntityToProductDto);
        }

        public async Task<ProductDto?> GetAsync(Expression<Func<Product, bool>> expression)
        {
            var product = await _unit.ProductRepository.GetAsync(expression);
            if (product is null) throw new NotFoundException("Sản phẩm không tồn tại");
            foreach (var variant in product.Variants)
            {
                variant.Color = await _colorService.GetColorsById(variant.ColorId);
                variant.Size = await _sizeService.GetSizesById(variant.SizeId);
            }
            return ProductMapper.EntityToProductDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryId(int categoryId)
        {
            var products = await _unit.ProductRepository.GetProductsAsync(p =>
                p.ProductCategories.Any(pc => pc.CategoryId == categoryId), false);

            if (products is null) throw new NotFoundException("Sản phẩm không tồn tại");
            foreach (var product in products)
            {
                foreach (var variant in product.Variants)
                {
                    variant.Color = await _colorService.GetColorsById(variant.ColorId);
                    variant.Size = await _sizeService.GetSizesById(variant.SizeId);
                }
            }
            return products.Select(ProductMapper.EntityToProductDto);
        }

        public async Task RemoveImageAsync(int productId, int imageId)
        {
            var product = await _unit.ProductRepository.GetAsync(r => r.Id == productId, true);
            if (product == null)
                throw new NotFoundException("product not found");

            var img = product.Image;
            if (img == null || img.Id != imageId)
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
            product.Image = null!;

            if (!await _unit.CompleteAsync())
            {
                throw new BadRequestException("Problem remove image product");
            }
        }

        public async Task<bool> RevertQuantityProductAsync(int orderId)
        {
            var order = await _unit.OrderRepository.GetAsync(o => o.Id == orderId);
            foreach (var item in order.OrderItems)
            {
                
                var variantOfProduct = await _unit.VariantRepository.GetAsync(v => v.ProductId == item.ProductId
                && v.Color.Name == item.ColorName && v.Size.Name == item.SizeName, true);

                // update
                variantOfProduct.Quantity += item.Quantity;
            }

            if (await _unit.CompleteAsync())
            {
                return true;
            }
            throw new BadRequestException("Cập nhật số lượng sản phẩm thất bại");

        }

        public async Task<ProductDto> UpdateAsync(ProductUpdate productUpdate)
        {
            if (!await _unit.ProductRepository.ExistsAsync(c => c.Id == productUpdate.Id))
                throw new NotFoundException("Sản phẩm không tồn tại");

            if (await _unit.ProductRepository.ExistsAsync(c => c.Name.ToLower() == productUpdate.Name.ToLower() && c.Id != productUpdate.Id))
                throw new BadRequestException("Sản phẩm đã tồn tại");

            var product = ProductMapper.ProductUpdateDtoToEntity(productUpdate);

            if (productUpdate.ImageFile != null)
            {
                if (product.Image != null && !string.IsNullOrEmpty(product.Image.PublicId))
                {
                    var deletionResult = await _cloudinaryService.DeleteImageAsync(product.Image.PublicId);
                    if (deletionResult.Error != null)
                    {
                        throw new BadRequestException("Lỗi khi xóa ảnh cũ");
                    }
                }

                var uploadResult = await _cloudinaryService.UploadImageAsync(productUpdate.ImageFile);
                if (uploadResult.Error != null)
                {
                    throw new BadRequestException("Lỗi khi thêm ảnh mới");
                }

                product.Image = new ProductImage
                {
                    ImgUrl = uploadResult.Url,
                    PublicId = uploadResult.PublicId,
                };
            }

            await _unit.ProductRepository.UpdateProductAsync(product);

            return await _unit.CompleteAsync()
                ? ProductMapper.EntityToProductDto(product)
                : throw new BadRequestException("Cập nhật sản phẩm thất bại");
        }

        public async Task<bool> UpdateQuantityProductAsync(Order order)
        {
            foreach (var item in order.OrderItems)
            {
                // check quantity
                var variantOfProduct = await _unit.VariantRepository.GetAsync(v => v.ProductId == item.ProductId
                && v.Color.Name == item.ColorName && v.Size.Name == item.SizeName, true);

                if (variantOfProduct == null)
                {
                    throw new BadRequestException($"Không tìm thấy loại sản phẩm có tên {item.ProductName}. Vui lòng xóa khỏi giỏ hàng và kiểm tra lại");
                }

                if (item.Quantity > variantOfProduct.Quantity)
                {
                    throw new BadRequestException($"Số lượng sản phẩm có tên {item.ProductName} chỉ còn lại {variantOfProduct.Quantity}. Vui lòng điều chỉnh lại số lượng sản phẩm");
                }

                // update
                variantOfProduct.Quantity -= item.Quantity;
            }

            if (await _unit.CompleteAsync())
            {
                return true;
            }
            throw new BadRequestException("Cập nhật số lượng sản phẩm thất bại");

        }


    }
}
