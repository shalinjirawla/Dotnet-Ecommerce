using ECommerce.Services.DTO;
using ECommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{

    [Route("api/products")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        #region Fields
        private readonly IProductService _productService;
        #endregion

        #region Constructor
        public ProductController(IProductService productService) 
        {  
            _productService = productService;
        }

        #endregion

        #region Methods

        [HttpGet("Products")]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }
        [HttpGet("Categories")]
        public IActionResult GetCategories()
        {
            return Ok(_productService.GetCategories());
        }
        [HttpGet("id")]
        public IActionResult GetProductById(int id)
        {
            return Ok(_productService.GetProductById(id));
        }
        [HttpGet("categoryId")]
        public IActionResult GetProductByCategoryId(int categoryId)
        {
            return Ok(_productService.GetProductsByCategoryId(categoryId));
        }
        [HttpPost("product")]
        public IActionResult AddProduct([FromForm]AddProductDTO product)
        {
            return Ok(_productService.AddProduct(product));
        }
        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDTO product)
        {
            return Ok(_productService.UpdateProduct(product));
        }
        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            return Ok(_productService.DeleteProduct(id));
        }

        #endregion
    }
}
