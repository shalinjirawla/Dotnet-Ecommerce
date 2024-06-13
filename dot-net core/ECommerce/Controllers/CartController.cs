using ECommerce.Models.Models;
using ECommerce.Services.DTO;
using ECommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/cart")]
    [ApiController]
    //[Authorize]
    public class CartController : ControllerBase
    {
        #region Fields
        private readonly ICartService _cartService;
        #endregion

        #region Constructors
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        #endregion

        #region Methods

        [HttpGet("id")]
        public IActionResult GetCartItemByUserId(int userId)
        {
            return Ok(_cartService.GetCartItemsByUserId(userId));
        }
        [HttpPost]
        public IActionResult AddToCart([FromBody] AddCartItemDTO cartItems)
        {
            return Ok(_cartService.AddToCart(cartItems.UserId,cartItems.ProductId,cartItems.Quantity));
        }
        [HttpDelete("id")]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            return Ok(_cartService.RemoveFromCart(cartItemId));
        }
        [HttpPut]
        public IActionResult UpdateCart(UpdateCartItemDTO cartItem)
        {
            return Ok(_cartService.UpdateCartItem(cartItem));
        }
        [HttpDelete("empty/{userId}")]
        public IActionResult EmptyCart(int userId)
        {
            return Ok(_cartService.EmptyCartItems(userId));
        }
        #endregion

    }
}
