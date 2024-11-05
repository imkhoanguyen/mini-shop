using API.Extensions;
using API.Helpers;
using API.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Shop.Application.DTOs.Reviews;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Entities;

namespace API.Controllers
{
    public class ReviewController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IHubContext<ReviewHub> _hub;
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService, UserManager<AppUser> userManager,
            ICloudinaryService cloudinaryService, IHubContext<ReviewHub> hub)
        {
            _reviewService = reviewService;
            _userManager = userManager;
            _cloudinaryService = cloudinaryService;
            _hub = hub;
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetReviews(int productId, [FromQuery] ReviewParams prm)
        {
            var pagedList = await _reviewService.GetAllAsync(productId, prm, false);

            Response.AddPaginationHeader(pagedList);

            return Ok(pagedList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromForm] ReviewCreateDto reviewCreateDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var review = await _reviewService.AddAsync(reviewCreateDto);
            await _hub.Clients.All.SendAsync("add-review", review);
            return CreatedAtAction(nameof(GetReviews), new {productId = review.ProductId}, review);
        }

        // update review & reply
        [HttpPut("{reviewId:int}")]
        public async Task<IActionResult> UpdateReview([FromRoute] int reviewId, [FromBody] ReviewEditDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = await _reviewService.UpdateAsync(dto);
            await _hub.Clients.All.SendAsync("edit-review", review);
            return Ok(review);
        }

        [HttpPost("add-images/{reviewId:int}")]
        public async Task<IActionResult> AddImagesReview([FromRoute] int reviewId, [FromForm] List<IFormFile> imageFiles)
        {
            var review = await _reviewService.AddImageAsync(reviewId, imageFiles);
            await _hub.Clients.All.SendAsync("edit-review", review);
            return Ok(review);
        }

        [HttpDelete("remove-image/{reviewId:int}")]
        public async Task<IActionResult> RemoveImagesReview([FromRoute] int reviewId, int imageId)
        {
            await _reviewService.RemoveImageAsync(reviewId, imageId);
            var review = await _reviewService.GetAsync(r => r.Id == reviewId);
            await _hub.Clients.All.SendAsync("edit-review", review);
            return NoContent();
        }

        [HttpPost("add-video/{reviewId:int}")]
        public async Task<IActionResult> AddVideoReview([FromRoute] int reviewId, [FromForm] IFormFile videoFile)
        {
            var review = await _reviewService.AddVideoAsync(reviewId, videoFile);
            await _hub.Clients.All.SendAsync("edit-review", review);
            return Ok(review);
        }

        [HttpDelete("remove-video/{reviewId:int}")]
        public async Task<IActionResult> RemoveVideoReview([FromRoute] int reviewId)
        {
            await _reviewService.RemoveVideoAsync(reviewId);
            var review = await _reviewService.GetAsync(r => r.Id == reviewId);
            await _hub.Clients.All.SendAsync("edit-review", review);
            return NoContent();
        }

        [HttpPost("add-reply")]
        public async Task<IActionResult> CreateReply([FromBody] ReplyCreateDto dto)
        {
           if(!ModelState.IsValid)
                return BadRequest(ModelState);

           var reply = await _reviewService.AddReplyAsync(dto);
            await _hub.Clients.All.SendAsync("add-reply", reply);
            return Ok(reply);

        }

        [HttpDelete("{reviewId:int}")]
        public async Task<IActionResult> RemoveReview([FromRoute] int reviewId)
        {
            await _reviewService.RemoveReview(reviewId);
            await _hub.Clients.All.SendAsync("delete-review", reviewId);
            return NoContent();
        }
    }
}
