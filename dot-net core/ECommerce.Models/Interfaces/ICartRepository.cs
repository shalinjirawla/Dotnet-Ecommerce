using ECommerce.Models.Models;

namespace ECommerce.Models.Interfaces
{
    public interface ICartRepository
    {
        CartItem GetCartItemById(int id);
        int AddToCart(CartItem cartItem);
        bool UpdateCartItem(CartItem cartItem);
        bool DeleteCartItem(CartItem cartItem);
        bool EmptyCart(int userId);
        IEnumerable<CartItem> GetCartItemByUserId(int userId);
        CartItem GetCartItem(int userId, int productId);
    }
}
