using API.Helpers;
using Shop.Application.DTOs.Products;
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
        public ProductService(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<ProductDto> AddAsync(ProductAdd productAdd)
        {
            if (await _unit.ProductRepository.ExistsAsync(c => c.Name.ToLower() == productAdd.Name.ToLower()))
            {
                throw new BadRequestException("Sản phẩm đã tồn tại");
            }
            var product = ProductMapper.ProductAddDtoToEntity(productAdd);
            await _unit.ProductRepository.AddAsync(product);

            return await _unit.CompleteAsync()
                ? ProductMapper.EntityToProductDto(product)
                : throw new BadRequestException("Thêm sản phẩm thất bại");
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
            var product = await _unit.ProductRepository.GetAllProductsAsync(productParams, tracked);

            var productDto = product.Select(ProductMapper.EntityToProductDto);
            return new PagedList<ProductDto>(productDto, product.TotalCount, productParams.PageNumber, productParams.PageSize);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(bool tracked)
        {
            var products = await _unit.ProductRepository.GetAllProductsAsync(tracked);
            return products.Select(ProductMapper.EntityToProductDto);
        }

        public async Task<ProductDto?> GetAsync(Expression<Func<Product, bool>> expression)
        {
            var product = await _unit.ProductRepository.GetAsync(expression);
            if (product is null) throw new NotFoundException("Sản phẩm không tồn tại");

            return ProductMapper.EntityToProductDto(product);
        }

        public async Task<ProductDto> UpdateAsync(ProductUpdate productUpdate)
        {
            if (!await _unit.ProductRepository.ExistsAsync(c => c.Id == productUpdate.Id))
                throw new NotFoundException("Sản phẩm không tồn tại");

            if (await _unit.ProductRepository.ExistsAsync(c => c.Name.ToLower() == productUpdate.Name.ToLower() && c.Id != productUpdate.Id))
                throw new BadRequestException("Sản phẩm đã tồn tại");

            var product = ProductMapper.ProductUpdateDtoToEntity(productUpdate);
            await _unit.ProductRepository.UpdateProductAsync(product);

            return await _unit.CompleteAsync()
                ? ProductMapper.EntityToProductDto(product)
                : throw new BadRequestException("Cập nhật sản phẩm thất bại");
        }
    }
}
