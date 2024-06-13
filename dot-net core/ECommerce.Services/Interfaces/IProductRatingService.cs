using ECommerce.Models.Models;
using ECommerce.Services.DTO;

namespace ECommerce.Services.Interfaces
{
    public interface IProductRatingService
    {
        ResponseDTO GetProductRatingById(int id);
        ResponseDTO GetProductRatingsByProductId(int productId);
        ResponseDTO GetProductRatingsByUserId(int userId);
        ResponseDTO AddProductRating(AddProductRatingDTO productRating);
        ResponseDTO UpdateProductRating(UpdateProductRatingDTO productRating);
        ResponseDTO DeleteProductRating(int id);
        ResponseDTO GetAverageProductRating(int productId);
        ResponseDTO GetAllProductsAverageRating();
    }
}
