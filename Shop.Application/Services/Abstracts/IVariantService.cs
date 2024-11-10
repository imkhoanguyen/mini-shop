using Microsoft.AspNetCore.Http;
using Shop.Application.DTOs.Reviews;
using Shop.Application.DTOs.Variants;
using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Application.Services.Abstracts
{
    public interface IVariantService
    {
        Task<VariantDto> AddAsync(VariantAdd variantAdd);
        Task<VariantDto> UpdateAsync(VariantUpdate variantUpdate);
        Task DeleteAsync(Expression<Func<Variant, bool>> expression);
        Task<VariantDto> AddImageAsync(int variantId, List<IFormFile> files);
        Task RemoveImageAsync(int variantId, int imageId);
        Task<VariantDto?> GetAsync(Expression<Func<Variant, bool>> expression);
        Task<IEnumerable<VariantDto>> GetByProductId(int productId);
    }
}
