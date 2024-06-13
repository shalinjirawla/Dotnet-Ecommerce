using ECommerce.Models.Models;

namespace ECommerce.Models.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int id);
        IEnumerable<Product> GetProductsByCategoryId(int categoryId);
        int AddProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        IEnumerable<Category> GetCategories();
        IEnumerable<ProductCategory> GetProductsCategories();
        Category GetCategoryById(int id);
    }
}
