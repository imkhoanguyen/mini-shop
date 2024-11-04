using Shop.Application.DTOs.Categories;
using Shop.Application.Mappers;
using Shop.Application.Parameters;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using System.Linq.Expressions;

namespace Shop.Application.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unit;

        public CategoryService(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<CategoryDto> AddAsync(CategoryAdd categoryAdd)
        {
            if(await _unit.CategoryRepository.ExistsAsync(c => c.Name.ToLower() == categoryAdd.Name.ToLower()))
            {
                throw new BadRequestException("Danh mục đã tồn tại");
            }
            var category = CategoryMapper.CategoryAddDtoToEntity(categoryAdd);
            await _unit.CategoryRepository.AddAsync(category);

            return await _unit.CompleteAsync() 
                ? CategoryMapper.EntityToCategoryDto(category) 
                : throw new BadRequestException("Thêm danh mục thất bại");

        }

        public async Task DeleteAsync(Expression<Func<Category, bool>> expression)
        {
            var category = await _unit.CategoryRepository.GetAsync(expression);
            if (category is null)
            {
                throw new NotFoundException("Danh mục không tồn tại");
            }
            await _unit.CategoryRepository.DeleteCategoryAsync(category);
            if(!await _unit.CompleteAsync())
            {
                throw new BadRequestException("Xóa danh mục thất bại");
            }
        }

        public async Task<PagedList<CategoryDto>> GetAllAsync(CategoryParams categoryParams, bool tracked)
        {
            var category = await _unit.CategoryRepository.GetAllCategoriesAsync(categoryParams, tracked);
            
            var categoryDto = category.Select(CategoryMapper.EntityToCategoryDto);
            return new PagedList<CategoryDto>(categoryDto, category.TotalCount, categoryParams.PageNumber, categoryParams.PageSize);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync(bool tracked)
        {
            var categorys = await _unit.CategoryRepository.GetAllCategoriesAsync(tracked);
            return categorys.Select(CategoryMapper.EntityToCategoryDto);
        }

        public async Task<CategoryDto?> GetAsync(Expression<Func<Category, bool>> expression)
        {
            var category = await _unit.CategoryRepository.GetAsync(expression);
            if(category is null) throw new NotFoundException("Danh mục không tồn tại");

            return CategoryMapper.EntityToCategoryDto(category);
        }

        public async Task<CategoryDto> UpdateAsync(CategoryUpdate categoryUpdate)
        {
            if(!await _unit.CategoryRepository.ExistsAsync(c => c.Id == categoryUpdate.Id))
                throw new NotFoundException("Danh mục không tồn tại");

            if(await _unit.CategoryRepository.ExistsAsync(c => c.Name.ToLower() == categoryUpdate.Name.ToLower()))
                throw new BadRequestException("Danh mục đã tồn tại");
            
            var category = CategoryMapper.CategoryUpdateDtoToEntity(categoryUpdate);
            await _unit.CategoryRepository.UpdateCategoryAsync(category);

            return await _unit.CompleteAsync()
                ? CategoryMapper.EntityToCategoryDto(category)
                : throw new BadRequestException("Cập nhật danh mục thất bại");
        }
    }
}
