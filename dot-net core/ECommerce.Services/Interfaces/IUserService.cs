using ECommerce.Models.Models;
using ECommerce.Services.DTO;

namespace ECommerce.Services.Interfaces
{
    public interface IUserService
    {
        ResponseDTO GetUsers();
        ResponseDTO GetUserbyId(int id);
        ResponseDTO GetUserbyEmail(string email);
        ResponseDTO AddUser(AddUserDTO user);
        User IsUserExist(AuthenticationDTO user);
    }
}
