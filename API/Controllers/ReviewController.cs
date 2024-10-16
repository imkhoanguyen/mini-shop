using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ReviewController : BaseApiController
    {
        private readonly IUnitOfWork _unit;

        public ReviewController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet("{productId:int}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews(int productId)
        {
            var reviews = await _unit.ReviewRepository.GetAllAsync(productId);

            return Ok(reviews);
        }
    }
}
