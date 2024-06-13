using AutoMapper;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Models;
using ECommerce.Services.DTO;
using ECommerce.Services.Interfaces;

namespace ECommerce.Services.Services
{
    public class UserService:IUserService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        #endregion

        #region Constructor
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        #endregion

        #region Methods
        public ResponseDTO GetUsers()
        {
            var response = new ResponseDTO();
            try
            {
                var data = _mapper.Map<List<User>>(_userRepository.GetUsers().ToList());
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

        public ResponseDTO GetUserbyId(int id)
        {

            var response = new ResponseDTO();
            try
            {
                var user = _userRepository.GetUserbyId(id);
                if (user == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "User not found";
                    return response;
                }
                var result = _mapper.Map<User>(user);

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

        public ResponseDTO GetUserbyEmail(string email)
        {

            var response = new ResponseDTO();
            try
            {
                var user = _userRepository.GetUserByEmail(email);
                if (user == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "User not found";
                    return response;
                }
                var result = _mapper.Map<User>(user);

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

        public ResponseDTO AddUser(AddUserDTO user)
        {

            var response = new ResponseDTO();
            try
            {
                var userByEmail = _userRepository.GetUserByEmail(user.Email);
                if (userByEmail != null)
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Email already exists";
                    return response;
                }
                var userId = _userRepository.AddUser(_mapper.Map<User>(user));
                if (userId == 0)
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Could not add user";
                    return response;
                }
                response.Status = 201;
                response.Message = "Created";
                response.Data = userId;
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;

        }
        public User IsUserExist(AuthenticationDTO user)
        {
            var result = _userRepository.GetUserByEmail(user.Email);
            if (result == null || result.Password != user.Password)
                return null;
            return _mapper.Map<User>(result);
        }
        #endregion
    }
}
