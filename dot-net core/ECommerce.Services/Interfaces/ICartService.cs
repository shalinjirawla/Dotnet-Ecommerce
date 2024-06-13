using ECommerce.Models.Models;
using ECommerce.Services.DTO;

namespace ECommerce.Services.Interfaces
{
    public interface ICartService
    {
        ResponseDTO AddToCart(int userId, int productId, int quantity);
        ResponseDTO RemoveFromCart(int cartItemId);
        ResponseDTO GetCartItemsByUserId(int userId);
        ResponseDTO GetCartItemById(int cartItemId);
        ResponseDTO GetCartItem(int userId, int productId);
        ResponseDTO UpdateCartItem(UpdateCartItemDTO cartItem);
        ResponseDTO EmptyCartItems(int userId);
    }
}
