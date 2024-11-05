using API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shop.Application.DTOs.Reviews;
using Shop.Application.Mappers;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using System.Linq.Expressions;

namespace Shop.Application.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unit;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICloudinaryService _cloudinaryService;

        public ReviewService(IUnitOfWork unit, UserManager<AppUser> userManager,
            ICloudinaryService cloudinaryService)
        {
            _unit = unit;
            _userManager = userManager;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ReviewDto> AddAsync(ReviewCreateDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                throw new BadRequestException("Không tìm thấy người dùng");


            var review = new Review
            {
                ProductId = dto.ProductId,
                ParentReviewId = dto.ParentReviewId,
                UserId = user.Id,
                Rating = dto.Rating,
                ReviewText = dto.ReviewText,
            };

            // add image file
            if (dto.ImageFile.Count > 0)
            {
                foreach (var file in dto.ImageFile)
                {
                    var resultUploadImg = await _cloudinaryService.UploadImageAsync(file);
                    if (resultUploadImg.Error != null)
                    {
                        throw new BadRequestException(resultUploadImg.Error);
                    }
                    var image = new ReviewImage
                    {
                        ImgUrl = resultUploadImg.Url,
                        PublicId = resultUploadImg.PublicId,
                    };
                    review.Images.Add(image);
                }
            }

            // add video file
            if (dto.VideoFile != null)
            {
                var resultUploadVideo = await _cloudinaryService.UploadVideoAsync(dto.VideoFile);
                if (resultUploadVideo.Error != null)
                {
                    throw new BadRequestException(resultUploadVideo.Error);
                }
                review.VideoUrl = resultUploadVideo.Url;
                review.PublicId = resultUploadVideo.PublicId;
            }

            await _unit.ReviewRepository.AddAsync(review);

            if (await _unit.CompleteAsync())
            {
                var reviewDto = ReviewMapper.EntityToReviewDto(review);
                return reviewDto;
            }

            throw new BadRequestException("Xảy ra lỗi khi thêm reivew");

        }

        public async Task<ReviewDto> AddImageAsync(int reviewId, List<IFormFile> files)
        {
            var review = await _unit.ReviewRepository.GetAsync(r => r.Id == reviewId, true);
            if (review == null)
                throw new NotFoundException("review notfond");

            if(files.Count < 1)
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
                    var img = new ReviewImage
                    {
                        ImgUrl = result.Url,
                        PublicId = result.PublicId,
                    };
                    review.Images.Add(img);
                }
            }

            if (await _unit.CompleteAsync())
            {
                return ReviewMapper.EntityToReviewDto(review);
            }

            throw new BadRequestException("Problem with add image of review");
        }

        public async Task<ReviewDto> AddReplyAsync(ReplyCreateDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                throw new NotFoundException("Không tìm thấy người dùng");


            var review = new Review
            {
                ProductId = dto.ProductId,
                ParentReviewId = dto.ParentReviewId,
                UserId = user.Id,
                ReviewText = dto.ReviewText,
            };

            await _unit.ReviewRepository.AddAsync(review);

            if (await _unit.CompleteAsync())
            {
                return ReviewMapper.EntityToReviewDto(review);
            }

            throw new BadRequestException("Xảy ra lỗi khi thêm reivew");
        }

        public async Task<ReviewDto> AddVideoAsync(int reviewId, IFormFile video)
        {
            var review = await _unit.ReviewRepository.GetAsync(r => r.Id == reviewId, true);
            if (review == null)
                throw new NotFoundException("review not fond");

            if (video == null) throw new BadRequestException("video is null");

            // remove video on cloudinary
            if (review.PublicId != null)
            {
                var resultDeleteVideo = await _cloudinaryService.DeleteImageAsync(review.PublicId);
                if (resultDeleteVideo.Error != null)
                {
                    throw new BadRequestException(resultDeleteVideo.Error);
                }
            }

            // add video on cloudinary
            var resultAdd = await _cloudinaryService.UploadVideoAsync(video);
            if (resultAdd.Error != null)
            {
                throw new BadRequestException(resultAdd.Error);
            }

            // update url & publicId of video

            review.VideoUrl = resultAdd.Url;
            review.PublicId = resultAdd.PublicId;

            if (await _unit.CompleteAsync())
            {
                return ReviewMapper.EntityToReviewDto(review);
            }

            throw new BadRequestException("Problem add video review");
        }

        public async Task<PagedList<ReviewDto>> GetAllAsync(int productId, ReviewParams prm, bool tracked = false)
        {
            var pagedList = await _unit.ReviewRepository.GetAllAsync(productId, prm, tracked);

            var reviewDtos = pagedList.Select(ReviewMapper.EntityToReviewDto);

            return new PagedList<ReviewDto>(reviewDtos, pagedList.TotalCount, pagedList.CurrentPage, pagedList.PageSize);
        }

        public async Task<ReviewDto> GetAsync(Expression<Func<Review, bool>> expression)
        {
            var review = await _unit.ReviewRepository.GetAsync(expression, false);
            return ReviewMapper.EntityToReviewDto(review);
        }

        public async Task RemoveImageAsync(int reviewId, int imageId)
        {
            var review = await _unit.ReviewRepository.GetAsync(r => r.Id == reviewId, true);
            if (review == null)
                throw new NotFoundException("review not fond");

            var img = review.Images.Find(i => i.Id == imageId);
            if (img == null)
                throw new NotFoundException("Image notfound");
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
            review.Images.Remove(img);

            if (!await _unit.CompleteAsync())
            {
                throw new BadRequestException("Problem remove image review");
            }
        }

        public async Task RemoveReview(int reviewId)
        {
            var review = await _unit.ReviewRepository.GetAsync(r => r.Id == reviewId);
            if (review == null)
                throw new NotFoundException("review notfond");

            // remove video of main review on cloudinary
            if (review.PublicId != null)
            {
                var resultDeleteVideoMain = await _cloudinaryService.DeleteImageAsync(review.PublicId);
                if (resultDeleteVideoMain.Error != null)
                {
                    throw new BadRequestException(resultDeleteVideoMain.Error);
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
                            throw new BadRequestException(resultDeleteImageMain.Error);
                        }
                    }
                }
            }

            _unit.ReviewRepository.Remove(review);

            if (!await _unit.CompleteAsync())
            {
                throw new BadRequestException("Problem with remove review");
            }
        }

        public async Task RemoveVideoAsync(int reviewId)
        {
            var review = await _unit.ReviewRepository.GetAsync(r => r.Id == reviewId, true);
            if (review == null)
                throw new NotFoundException("review notfond");

            // remove video on cloudinary
            if (review.PublicId != null)
            {
                var resultDeleteVideo = await _cloudinaryService.DeleteImageAsync(review.PublicId);
                if (resultDeleteVideo.Error != null)
                {
                    throw new BadRequestException(resultDeleteVideo.Error);
                }
            }

            review.PublicId = null;
            review.VideoUrl = null;

            if (!await _unit.CompleteAsync())
            {
                throw new BadRequestException("Problem with remove video review");
            }
        }

        public async Task<ReviewDto> UpdateAsync(ReviewEditDto dto)
        {
            var review = await _unit.ReviewRepository.GetAsync(r => r.Id == dto.Id, true);
            if (review == null)
                throw new NotFoundException("review not found");

            review.ReviewText = dto.ReviewText;
            if (review.Rating != null)
                review.Rating = dto.Rating;


            if (await _unit.CompleteAsync())
            {
                return ReviewMapper.EntityToReviewDto(review);
            }

            throw new BadRequestException("Problem update review");
        }


    }
}
