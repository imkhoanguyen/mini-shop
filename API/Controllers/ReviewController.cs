using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ReviewController : BaseApiController
    {
        private readonly IUnitOfWork _unit;
        private readonly UserManager<AppUser> _userManager;

        public ReviewController(IUnitOfWork unit, UserManager<AppUser> userManager)
        {
            _unit = unit;
            _userManager = userManager;
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetAllReviews(int productId)
        {
            var reviews = await _unit.ReviewRepository.GetAllAsync(productId);

            var reviewDtos = reviews.Select(ReviewDto.FromEntity).ToList();

            return Ok(reviewDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromForm] ReviewCreateDto reviewCreateDto)
        {
            var user = await _userManager.FindByIdAsync(reviewCreateDto.UserId);
            if (user == null)
                return BadRequest("Không tìm thấy người dùng");


            var review = new Review
            {
                ProductId = reviewCreateDto.ProductId,
                ParentReviewId = reviewCreateDto.ParentReviewId,
                UserId = user.Id,
                Rating = reviewCreateDto.Rating,
                ReviewText = reviewCreateDto.ReviewText,
            };

            _unit.ReviewRepository.Add(review);

            if (await _unit.Complete())
            {
                var reviewDto = ReviewDto.FromEntity(review);
                return Ok(new { message = "Review added successfully", reviewDto });
            }

            return BadRequest("Xảy ra lỗi khi thêm reivew");

        }
    }
}
