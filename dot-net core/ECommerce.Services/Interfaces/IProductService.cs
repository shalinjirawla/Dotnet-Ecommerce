using ECommerce.Models.Models;
using ECommerce.Services.DTO;

namespace ECommerce.Services.Interfaces
{
    public interface IProductService
    {
        ResponseDTO GetProducts();
        ResponseDTO GetProductById(int id);
        ResponseDTO GetProductsByCategoryId(int categoryId);
        ResponseDTO AddProduct(AddProductDTO product);
        ResponseDTO UpdateProduct(UpdateProductDTO updateProduct);
        ResponseDTO DeleteProduct(int id);
        ResponseDTO GetCategories();
        ResponseDTO GetProductCategories();
        ResponseDTO GetCategoryById(int id);

    }
}
