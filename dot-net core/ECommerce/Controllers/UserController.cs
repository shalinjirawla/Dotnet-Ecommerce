using ECommerce.Services.DTO;
using ECommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommBackend.Controllers
{
    [Route("api/users")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        #region Fields
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion
        #region Methods

        [HttpGet("Users")]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetUsers());
        }
        [HttpGet("id")]
        public IActionResult GetUserById(int id)
        {
            return Ok(_userService.GetUserbyId(id));
        }

        [HttpPost("user")]
        [AllowAnonymous]
        public IActionResult AddUser(AddUserDTO user)
        {

            return Ok(_userService.AddUser(user));
        }
        #endregion
    }
}