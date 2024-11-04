using API.Controllers;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services.Abstracts;

namespace api.Controllers
{
    public class ProductController : BaseApiController
    {
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly ICloudinaryService _imageService;

        //public ProductController(IUnitOfWork unitOfWork, ICloudinaryService imageService)
        //{
        //    _unitOfWork = unitOfWork;
        //    _imageService = imageService;
        //}

        //[HttpGet("GetProductById{id}")]
        //public async Task<IActionResult> GetProductByIdAsync(int id)
        //{
        //    var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(id);
        //    if (product == null) return NotFound("Không tìm thấy sản phẩm nào.");
        //    var productDto = Product.toProductGetDto(product);
        //    return Ok(productDto);
        //}
        //[HttpGet("GetByName")]
        //public async Task<IActionResult> GetProductByName(string name)
        //{
        //    var product = await _unitOfWork.ProductRepository.GetProductByName(name);
        //    if (product == null) return NotFound("không tìm thấy sản phẩm với tên = " + name);
        //    var productDto = Product.toProductGetDto(product);
        //    return Ok(productDto);
        //}
        //[HttpGet("GetAll")]
        //public async Task<IActionResult> GetAllProductsAsync()
        //{
        //    var product = await _unitOfWork.ProductRepository.GetAllProductsAsync();
        //    if (product == null)
        //    {
        //        return NotFound("Không tìm thấy sản phẩm nào.");
        //    }
        //    var productDto = product.Select(p => Product.toProductGetDto(p)).ToList();
        //    return Ok(productDto);
        //}
        
        //[HttpGet("GetAllPaging")]
        //public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllProductsAsync([FromQuery] ProductParams productParams)
        //{
        //    var pageList = await _unitOfWork.ProductRepository.GetAllProductsAsync(productParams);
        //    if (pageList == null)
        //    {
        //        return NotFound("Không tìm thấy sản phẩm nào.");
        //    }
        //     var pageListDto = new PageListDto<ProductGetDto>
        //    {
        //        PageIndex = pageList.PageIndex,
        //        PageSize = pageList.PageSize,
        //        TotalCount = pageList.TotalCount,
        //        TotalPages = pageList.TotalPages,
        //        Items = pageList.Select(c => Product.toProductGetDto(c)).ToList()
        //    };
        //    return Ok(pageListDto);
        //}

        //// POST api/Product/Add
        //[HttpPost("Add")]
        //public async Task<IActionResult> AddProduct([FromBody] ProductAddDto productAddDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (await _unitOfWork.ProductRepository.ProductExistsAsync(productAddDto.Name!))
        //    {
        //        return BadRequest(new {message ="Sản phẩm với tên này đã tồn tại."});
        //    }
        //    var product = ProductAddDto.toProduct(productAddDto);
        //    _unitOfWork.ProductRepository.AddProduct(product);

        //    if (await _unitOfWork.Complete()){
        //        await _unitOfWork.ProductRepository.AddProductCategory(product);
        //        return Ok(new {id = product.Id, message ="Add Product successfully."});
        //    }
        //    return BadRequest(new {message ="Add Product failed."});
        //}

        //// PUT api/Product/Update
        //[HttpPut("Update")]
        //public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var product = ProductDto.toProduct(productDto);
        //    await _unitOfWork.ProductRepository.AddProductCategory(product);
        //    await _unitOfWork.ProductRepository.UpdateProduct(product);
            
        //    if (await _unitOfWork.Complete()){
                
        //        return Ok(new {message = "Update Product successfully."});
        //    }
        //    return BadRequest(new {message = "Update Product failed."});
        //}

        //// DELETE api/Product/Delete
        //[HttpDelete("Delete")]
        //public async Task<IActionResult> DeleteProduct([FromBody]ProductDto productDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var product = ProductDto.toProduct(productDto);
        //    _unitOfWork.ProductRepository.DeleteProduct(product);

        //    if (await _unitOfWork.Complete())
        //        return Ok(new {message = "Delete Product successfully."});
        //    return BadRequest(new {message = "Delete Product failed."});
            
        //}
        
    }
}
