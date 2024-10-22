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
        private readonly ICloudinaryService _cloudinaryService;

        public ReviewController(IUnitOfWork unit, UserManager<AppUser> userManager, ICloudinaryService cloudinaryService)
        {
            _unit = unit;
            _userManager = userManager;
            _cloudinaryService = cloudinaryService;
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

            // add image file
            if (reviewCreateDto.ImageFile.Count > 0)
            {
                foreach (var file in reviewCreateDto.ImageFile)
                {
                    var resultUploadImg = await _cloudinaryService.UploadImageAsync(file);
                    if (resultUploadImg.Error != null)
                    {
                        return BadRequest(resultUploadImg.Error.Message);
                    }
                    var image = new ReviewImage
                    {
                        ImgUrl = resultUploadImg.SecureUrl.ToString(),
                        PublicId = resultUploadImg.PublicId,
                    };
                    review.Images.Add(image);
                }
            }

            // add video file
            if (reviewCreateDto.VideoFile != null)
            {
                var resultUploadVideo = await _cloudinaryService.UploadVideoAsync(reviewCreateDto.VideoFile);
                if (resultUploadVideo.Error != null)
                {
                    return BadRequest(resultUploadVideo.Error.Message);
                }
                review.VideoUrl = resultUploadVideo.SecureUrl.ToString();
                review.PublicId = resultUploadVideo.PublicId;
            }

            _unit.ReviewRepository.Add(review);

            if (await _unit.Complete())
            {
                var reviewDto = ReviewDto.FromEntity(review);
                return Ok(new { message = "Review added successfully", reviewDto });
            }

            return BadRequest("Xảy ra lỗi khi thêm reivew");

        }

        [HttpPut("{reviewId:int}")]
        public async Task<IActionResult> UpdateReview([FromRoute] int reviewId, [FromBody] ReviewEditDto dto)
        {
            var review = _unit.ReviewRepository.GetAsync(dto.Id);

            if (review == null) return BadRequest("review notfound");

            await _unit.ReviewRepository.UpdateAsync(dto);

            if (await _unit.Complete())
            {
                return NoContent();
            }

            return BadRequest("Problem update review");
        }

        [HttpPost("add-images/{reviewId:int}")]
        public async Task<IActionResult> AddImagesReview([FromRoute] int reviewId, [FromForm] List<IFormFile> imageFiles)
        {
            var review = await _unit.ReviewRepository.GetAsync(reviewId);
            if (review == null) return BadRequest("review notfond");

            if (imageFiles.Count > 0)
            {
                foreach (var file in imageFiles)
                {
                    var result = await _cloudinaryService.UploadImageAsync(file);
                    if (result.Error != null)
                    {
                        return BadRequest(result.Error.Message);
                    }
                    var img = new ReviewImage
                    {
                        ImgUrl = result.SecureUrl.ToString(),
                        PublicId = result.PublicId,
                    };
                    review.Images.Add(img);
                }
            }

            if (await _unit.Complete())
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("remove-image/{reviewId:int}")]
        public async Task<IActionResult> RemoveImagesReview([FromRoute] int reviewId, int imageId)
        {
            var review = await _unit.ReviewRepository.GetAsync(reviewId);
            if (review == null) return BadRequest("review notfond");

            var img = review.Images.Find(i => i.Id == imageId);
            if (img == null) return BadRequest("Image notfound");
            // remove image on cloudinary
            if (img.PublicId != null)
            {
                var result = await _cloudinaryService.DeleteImageAsync(img.PublicId);
                if (result.Error != null)
                {
                    return BadRequest(result.Error.Message);
                }
            }

            //remove image on db
            review.Images.Remove(img);

            if (await _unit.Complete())
            {
                return NoContent();
            }

            return BadRequest("Problem remove image review");
        }

        [HttpPost("add-video/{reviewId:int}")]
        public async Task<IActionResult> AddVideoReview([FromRoute] int reviewId, [FromForm] IFormFile videoFile)
        {
            var review = await _unit.ReviewRepository.GetAsync(reviewId);
            if (review == null) return BadRequest("review notfond");

            if (videoFile == null) return BadRequest("video is null");

            // remove video on cloudinary
            if (review.PublicId != null)
            {
                var resultDeleteVideo = await _cloudinaryService.DeleteImageAsync(review.PublicId);
                if (resultDeleteVideo.Error != null)
                {
                    return BadRequest(resultDeleteVideo.Error.Message);
                }
            }

            // add video on cloudinary
            var resultAdd = await _cloudinaryService.UploadVideoAsync(videoFile);
            if (resultAdd.Error != null)
            {
                return BadRequest(resultAdd.Error.Message);
            }

            // update url & publicId of video

            review.VideoUrl = resultAdd.SecureUrl.ToString();
            review.PublicId = resultAdd.PublicId;

            if (await _unit.Complete())
            {
                return NoContent();
            }

            return BadRequest("Problem add video review");
        }

        [HttpDelete("remove-video/{reviewId:int}")]
        public async Task<IActionResult> RemoveVideoReview([FromRoute] int reviewId)
        {
            var review = await _unit.ReviewRepository.GetAsync(reviewId);
            if (review == null) return BadRequest("review notfond");

            // remove video on cloudinary
            if (review.PublicId != null)
            {
                var resultDeleteVideo = await _cloudinaryService.DeleteImageAsync(review.PublicId);
                if (resultDeleteVideo.Error != null)
                {
                    return BadRequest(resultDeleteVideo.Error.Message);
                }
            }

            review.PublicId = null;
            review.VideoUrl = null;

            if (await _unit.Complete())
            {
                return NoContent();
            }

            return BadRequest("Problem with remove video review");
        }

        [HttpPost("add-reply")]
        public async Task<IActionResult> CreateReview([FromBody] ReplyCreateDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                return BadRequest("Không tìm thấy người dùng");


            var review = new Review
            {
                ProductId = dto.ProductId,
                ParentReviewId = dto.ParentReviewId,
                UserId = user.Id,
                ReviewText = dto.ReviewText,
            };

            _unit.ReviewRepository.Add(review);

            if (await _unit.Complete())
            {
                var reviewDto = ReviewDto.FromEntity(review);
                return Ok(new { message = "Repply added successfully", reviewDto });
            }

            return BadRequest("Xảy ra lỗi khi thêm reivew");

        }

        [HttpDelete("{reviewId:int}")]
        public async Task<IActionResult> RemoveReview([FromRoute] int reviewId)
        {
            var review = await _unit.ReviewRepository.GetAsync(reviewId);
            if (review == null) return BadRequest("review notfond");

            // remove video of main review on cloudinary
            if (review.PublicId != null)
            {
                var resultDeleteVideoMain = await _cloudinaryService.DeleteImageAsync(review.PublicId);
                if (resultDeleteVideoMain.Error != null)
                {
                    return BadRequest(resultDeleteVideoMain.Error.Message);
                }
            }

            // remove images of main review on cloudinary
            if (review.Images.Count > 0)
            {
                foreach (var image in review.Images)
                {
                    if (image.PublicId != null)
                    {
                        var resultDeleteImageMain = await _cloudinaryService.DeleteImageAsync(image.PublicId);
                        if (resultDeleteImageMain.Error != null)
                        {
                            return BadRequest(resultDeleteImageMain.Error.Message);
                        }
                    }
                }
            }

            _unit.ReviewRepository.Delete(review);

            if(await _unit.Complete())
            {
                return NoContent();
            }

            return BadRequest("Problem with remove review");
        }
    }
}
