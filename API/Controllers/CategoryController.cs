using API.Controllers;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/category/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllCategoriesAsync();
            if (categories == null)
            {
                return NotFound("Không tìm thấy danh mục nào.");
            }
            var categoryDto = categories.Select(c => Category.toCategoryDto(c)).ToList();
            return Ok(categoryDto);
        }
        // GET api/category/GetById
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetCategoriesById(int id)
        {
            var categories = await _unitOfWork.CategoryRepository.GetCategoriesById(id);
            if (categories == null)
            {
                return NotFound("Không tìm thấy danh mục nào.");
            }
            var categoryDto = Category.toCategoryDto(categories);
            return Ok(categoryDto);
        }
        [HttpGet("GetCategoryNameById/{id}")]
        public async Task<IActionResult> GetCategoryNameById(int id)
        {
            var categoryName = await _unitOfWork.CategoryRepository.GetCategoryNameById(id);

            if (categoryName == null)
            {
                return NotFound("Không tìm thấy danh mục nào.");
            }

            return Ok(new { message=categoryName});
        }

        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<PageListDto<CategoryDto>>> GetAllCategoriesAsync([FromQuery] CategoryParams categoryParams)
        {
            var pageList = await _unitOfWork.CategoryRepository.GetAllCategoriesAsync(categoryParams);

            // Kiểm tra xem danh sách có rỗng hay không
            if (pageList == null || !pageList.Any())
            {
                return NotFound("Không tìm thấy danh mục nào.");
            }

            // Trả về PageListDto
            var pageListDto = new PageListDto<CategoryDto>
            {
                PageIndex = pageList.PageIndex,
                PageSize = pageList.PageSize,
                TotalCount = pageList.TotalCount,
                TotalPages = pageList.TotalPages,
                Items = pageList.Select(c => Category.toCategoryDto(c)).ToList()
            };
            return Ok(pageListDto);
        }
        // POST api/category/Add
        [HttpPost("Add")]
        public async Task<ActionResult> AddCategory([FromBody] CategoryAddDto categoryAddDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (categoryAddDto.Name == null)
            {
                return BadRequest(new { message = "Tên danh mục không được để trống." });
            }
            if (await _unitOfWork.CategoryRepository.CategoryExistsAsync(categoryAddDto.Name!))
            {
                return BadRequest(new { message = "Danh mục với tên này đã tồn tại." });
            }
            var category = CategoryAddDto.toCategory(categoryAddDto);
            _unitOfWork.CategoryRepository.AddCategory(category);

            if (await _unitOfWork.Complete())
                return Ok(new { message = "Thêm danh mục thành công." });
            return BadRequest(new { message = "Thêm danh mục thất bại." });
        }

        // PUT api/category/Update
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (categoryDto.Name == null)
            {
                return BadRequest(new { message = "Tên danh mục không được để trống." });
            }
            if (await _unitOfWork.CategoryRepository.CategoryExistsAsync(categoryDto.Name!))
            {
                return BadRequest("Danh mục với tên này đã tồn tại.");
            }
            var category = CategoryDto.toCategory(categoryDto);
            _unitOfWork.CategoryRepository.UpdateCategory(category);

            if (await _unitOfWork.Complete())
            {
                return Ok(new { message = "Cập nhật danh mục thành công." });
            }

            return BadRequest("Cập nhật danh mục thất bại.");
        }

        // DELETE api/category/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCategory(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = CategoryDto.toCategory(categoryDto);
            _unitOfWork.CategoryRepository.DeleteCategory(category);

            if (await _unitOfWork.Complete())
                return Ok(new { message = "Cập nhật danh mục thành công." });

            return BadRequest("Cập nhật danh mục thất bại");
        }
    }
}
