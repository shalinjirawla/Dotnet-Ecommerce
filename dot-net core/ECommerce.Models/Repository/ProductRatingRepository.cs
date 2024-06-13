using ECommerce.Models.Interfaces;
using ECommerce.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Models.Repository
{
    public class ProductRatingRepository:IProductRatingRepository
    {
        #region Fields
        private readonly AppDbContext _context;
        #endregion

        #region Constructors
        public ProductRatingRepository(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods
        public ProductRating GetProductRatingById(int id)
        {
            return _context.ProductRatings.Include(u=>u.User).Include(p=>p.Product).FirstOrDefault(pr => pr.Id == id);
        }

        public IEnumerable<ProductRating> GetProductRatingsByProductId(int productId)
        {
            return _context.ProductRatings.Where(pr => pr.ProductId == productId).ToList();
        }
        public IEnumerable<ProductRating> GetProductRatingByUserId(int userId)
        {
            return _context.ProductRatings.Where(pr => pr.UserId == userId).ToList();
        }
        public int AddProductRating(ProductRating productRating)
        {
            _context.Add(productRating);
            if (_context.SaveChanges() > 0)
                return productRating.Id;
            else
                return 0;
        }
        public bool UpdateProductRating(ProductRating productRating)
        {
            var existingProductRating = _context.ProductRatings.Find(productRating.Id);
            if (existingProductRating != null)
            {
                existingProductRating.Rating = productRating.Rating;
                _context.Entry(existingProductRating).State = EntityState.Modified;
            }
            return _context.SaveChanges() > 0;
        }

        public bool DeleteProductRating(ProductRating productRating)
        {
            _context.Remove(productRating);
            return _context.SaveChanges() > 0;
        }

        public double GetAverageProductRating(int productId)
        {
            var productRatings = _context.ProductRatings
                .Where(pr => pr.ProductId == productId)
                .Select(pr => pr.Rating);

            if (productRatings.Any())
            {
                return productRatings.Average();
            }

            return 0; 
        }

        public double GetAllProductsAverageRating()
        {
            try
            {
                var productRatings = _context.ProductRatings
                    .GroupBy(pr => pr.ProductId)
                    .Select(g => new
                    {
                        ProductId = g.Key,
                        AverageRating = g.Average(pr => pr.Rating)
                    })
                    .ToList();

                if (productRatings.Any())
                {
                    return productRatings.Average(pr => pr.AverageRating);
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
    
    


