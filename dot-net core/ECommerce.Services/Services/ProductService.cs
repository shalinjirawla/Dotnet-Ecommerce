using AutoMapper;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Models;
using ECommerce.Services.DTO;
using ECommerce.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace ECommerce.Services.Services
{
    public class ProductService:IProductService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository ;
        private readonly string _uploadsFolderPath;
        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion
        #region Constructors
        public ProductService(IMapper mapper, IProductRepository productRepository, string uploadsFolderPath,IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _uploadsFolderPath = uploadsFolderPath;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Methods
        public ResponseDTO GetProducts()
        {
            var response = new ResponseDTO();
            try
            {
            
                var data = _mapper.Map<List<Product>>(_productRepository.GetProducts().ToList());
                response.Status = 200;
                response.Message = "Ok";
                response.Data = data;
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }

        public ResponseDTO GetProductById(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var product = _productRepository.GetProductById(id);
                if (product == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Product not found";
                    return response;
                }
                var result = _mapper.Map<Product>(product);

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
        public ResponseDTO GetProductsByCategoryId(int categoryId)
        {
            var response = new ResponseDTO();
            try
            {
                var products = _productRepository.GetProductsByCategoryId(categoryId);

                if (products == null || !products.Any())
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Products not found for the specified category ID";
                    return response;
                }
                var productDTOs = _mapper.Map<List<Product>>(products);

                response.Status = 200;
                response.Message = "Ok";
                response.Data = productDTOs;
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }

        public ResponseDTO AddProduct(AddProductDTO product)
        {
            var response = new ResponseDTO();
            try
            {
                // product image type check
                if(product.ProductPhotoUrl != null && !product.ProductPhotoUrl.ContentType.Contains("jpeg") && !product.ProductPhotoUrl.ContentType.Contains("png"))
                {
                    response.Status = 400;
                    response.Message = "Invalid  File format , only jpeg and png images are allowed";
                    response.Error = "Invalid type";
                }
                // Save product image 

                FileInfo productImage = new FileInfo(product.ProductPhotoUrl.FileName);
                var uploadImage = Path.Combine("wwwroot", "ProductImage");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productImage.Name);
                string filePath = Path.Combine(uploadImage,fileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.ProductPhotoUrl.CopyTo(fileStream);
                }
                string relativeFilePath = filePath.Substring(filePath.IndexOf("ProductImage"));

                // image to convert base64
                byte[] imgData;
                using(var fs = new FileStream(filePath, FileMode.Open))
                {
                    using (var ms =  new MemoryStream())
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, bytesRead);
                        }
                        imgData = ms.ToArray();
                    }
                }
                string baseImage = Convert.ToBase64String(imgData);
                var productData = new Product
                {
                    ProductName = product.ProductName,
                    ProductPhotoUrl = "data:image/jpeg;base64," + baseImage,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    ProductCategories = product.ProductCategories?.Select(Category => new ProductCategory{CategoryId = Category}).ToList(),

                };

                var newProduct = _mapper.Map<Product>(productData);
                var productId = _productRepository.AddProduct(newProduct);

                if (productId > 0)
                {
                    response.Status = 200;
                    response.Message = "Product added successfully";
                    response.Data = productData;
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Failed to add product";
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

        public ResponseDTO UpdateProduct(UpdateProductDTO product)
        {
            var response = new ResponseDTO();
            try
            {
                var productById = _productRepository.GetProductById(product.Id);
                if (productById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "product not found";
                    return response;
                }
                var updateFlag = _productRepository.UpdateProduct(_mapper.Map<Product>(product));
                if (updateFlag)
                {
                    response.Status = 204;
                    response.Message = "Updated";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Could not update product";
                }
            }
            catch(Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }

        public ResponseDTO DeleteProduct(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var productById = _productRepository.GetProductById(id);
                if (productById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "User not found";
                    return response;
                }
                var deleteFlag = _productRepository.DeleteProduct(productById);
                if (deleteFlag)
                {
                    response.Status = 204;
                    response.Message = "Deleted";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Deleted";
                    response.Error = "Could not delete user";
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

        public ResponseDTO GetCategories()
        {

            var response = new ResponseDTO();
            try
            {
                var data = _mapper.Map<List<Category>>(_productRepository.GetCategories().ToList());
                response.Status = 200;
                response.Message = "Ok";
                response.Data = data;
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }

        public ResponseDTO GetProductCategories()
        {

            var response = new ResponseDTO();
            try
            {
                var data = _mapper.Map<List<ProductCategory>>(_productRepository.GetProductsCategories().ToList());
                response.Status = 200;
                response.Message = "Ok";
                response.Data = data;
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }

        public ResponseDTO GetCategoryById(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var category = _productRepository.GetCategoryById(id);
                if (category == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Category not found";
                    return response;
                }
                var result = _mapper.Map<Category>(category);

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
        #endregion
    }
}
