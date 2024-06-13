using ECommerce.Services.DTO;
using ECommerce.Services.Interfaces;
using ECommerce.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/rating")]
    [ApiController]
    public class ProductRatingController : ControllerBase
    {
        #region fields
        private readonly IProductRatingService _productRatingService;
        #endregion
        #region Constructors
        public ProductRatingController(IProductRatingService productRatingService)
        {
            _productRatingService = productRatingService;
        }
        #endregion

        #region Methods
        [HttpGet("id")]
        public IActionResult GetProductRatingById(int id)
        {
            return Ok(_productRatingService.GetProductRatingById(id));
        }
        [HttpGet("product/{productId}")]
        public IActionResult GetProductRatingsByProductId(int productId)
        {
            return Ok(_productRatingService.GetProductRatingsByProductId(productId));
        }
        [HttpGet("products/{userId}")]
        public IActionResult GetProductRatingsByUserId(int userId)
        {
            return Ok(_productRatingService.GetProductRatingsByUserId(userId));
        }
        [HttpGet("average/{productId}")]
        public IActionResult GetAverageProductRating(int productId)
        {
            return Ok(_productRatingService.GetAverageProductRating(productId));
        }
        [HttpGet("average/all")]
        public IActionResult GetAllProductsAverageRating()
        {
            return Ok(_productRatingService.GetAllProductsAverageRating());
        }
        [HttpPost]
        public IActionResult AddProductRating(AddProductRatingDTO productRating)
        {
            return Ok(_productRatingService.AddProductRating(productRating));
        }

        [HttpPut]
        public IActionResult UpdateProductRating(UpdateProductRatingDTO productRating)
        {
            return Ok(_productRatingService.UpdateProductRating(productRating));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProductRating(int id)
        {
            return Ok(_productRatingService.DeleteProductRating(id));
        }
        #endregion
    }
}
