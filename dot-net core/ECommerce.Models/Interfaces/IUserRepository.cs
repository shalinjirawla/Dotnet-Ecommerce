using ECommerce.Models.Models;

namespace ECommerce.Models.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserbyId(int id);
        User GetUserByEmail(string email);
        int AddUser(User user);
    }
}
