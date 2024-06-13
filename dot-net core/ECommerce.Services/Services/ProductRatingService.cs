using AutoMapper;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Models;
using ECommerce.Models.Repository;
using ECommerce.Services.DTO;
using ECommerce.Services.Interfaces;

namespace ECommerce.Services.Services
{
    public class ProductRatingService:IProductRatingService
    {
        #region Fields
        private readonly IProductRatingRepository _productRatingRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        #endregion

        #region Constructor
        public ProductRatingService(IMapper mapper,IProductRatingRepository productRatingRepository,IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRatingRepository = productRatingRepository;
            _productRepository = productRepository;
        }
        #endregion

        #region Methods
        public ResponseDTO GetProductRatingById(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var productRating = _productRatingRepository.GetProductRatingById(id);
                if (productRating == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Product rating not found";
                    return response;
                }
                var result = _mapper.Map<ProductRating>(productRating);

                response.Status = 200;
                response.Message = "Ok";
                response.Data = result;
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }
        public ResponseDTO GetProductRatingsByProductId(int productId)
        {
            var response = new ResponseDTO();
            try
            {
                var productRatings = _productRatingRepository.GetProductRatingsByProductId(productId);
                if (productRatings == null || !productRatings.Any())
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Product ratings not found for the specified product ID";
                    return response;
                }
                var productRatingsData = _mapper.Map<List<ProductRating>>(productRatings);

                response.Status = 200;
                response.Message = "Ok";
                response.Data = productRatingsData;
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }

        public ResponseDTO GetProductRatingsByUserId(int userId)
        {
            var response = new ResponseDTO();
            try
            {
                var productRatings = _productRatingRepository.GetProductRatingByUserId(userId);
                if (productRatings == null || !productRatings.Any())
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Product ratings not found for the specified user Id";
                    return response;
                }
                var productRatingsData = _mapper.Map<List<ProductRating>>(productRatings);

                response.Status = 200;
                response.Message = "Ok";
                response.Data = productRatingsData;
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }

        public ResponseDTO AddProductRating(AddProductRatingDTO productRating)
        {
            var response = new ResponseDTO();
            try
            {
                var product = _productRepository.GetProductById(productRating.ProductId);
                if (product == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Product not found";
                    return response;
                }

                var productRatingData = _mapper.Map<ProductRating>(productRating);
                var productRatingId = _productRatingRepository.AddProductRating(productRatingData);

                if (productRatingId > 0)
                {
                    response.Status = 200;
                    response.Message = "Product rating added successfully";
                    response.Data = productRatingId;
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Failed to add product rating";
                }
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }
        public ResponseDTO UpdateProductRating(UpdateProductRatingDTO productRating)
        {
            var response = new ResponseDTO();
            try
            {
                var existingProductRating = _productRatingRepository.GetProductRatingById(productRating.Id);
                if (existingProductRating == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Product rating not found";
                    return response;
                }

                var productRatingData = _mapper.Map<ProductRating>(productRating);
                if (!_productRatingRepository.UpdateProductRating(productRatingData))
                {
                    response.Status = 400;
                    response.Message = "Failed to update product rating";
                    return response;
                }

                response.Status = 200;
                response.Message = "Product rating updated successfully";
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }
        public ResponseDTO DeleteProductRating(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var productRating = _productRatingRepository.GetProductRatingById(id);
                if (productRating == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Product rating not found";
                    return response;
                }

                if (!_productRatingRepository.DeleteProductRating(productRating))
                {
                    response.Status = 400;
                    response.Message = "Failed to delete product rating";
                    return response;
                }

                response.Status = 200;
                response.Message = "Product rating deleted successfully";
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }

        public ResponseDTO GetAverageProductRating(int productId)
        {
            var response = new ResponseDTO();

            try
            {
                var productRatings = _productRatingRepository.GetProductRatingsByProductId(productId);

                if (productRatings.Any())
                {
                    var averageRating = productRatings.Average(pr => pr.Rating);
                    response.Status = 200;
                    response.Message = "Ok";
                    response.Data = averageRating;
                }
                else
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "No ratings found for the specified product ID";
                }
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }

            return response;
        }

        public ResponseDTO GetAllProductsAverageRating()
        {
            var response = new ResponseDTO();

            try
            {
                var products = _productRepository.GetProducts();

                if (products.Any())
                {
                    var averageRatings = new Dictionary<int, double>();

                    foreach (var product in products)
                    {
                        var productRatings = _productRatingRepository.GetProductRatingsByProductId(product.Id);

                        if (productRatings.Any())
                        {
                            averageRatings.Add(product.Id, productRatings.Average(pr => pr.Rating));
                        }
                        else
                        {
                            averageRatings.Add(product.Id, 0);
                        }
                    }

                    response.Status = 200;
                    response.Message = "Ok";
                    response.Data = averageRatings;
                }
                else
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "No products found";
                }
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }

            return response;
        }
        #endregion
    }
}
