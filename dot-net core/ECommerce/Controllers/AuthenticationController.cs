using ECommerce.Services.DTO;
using ECommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommBackend.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        #region Fields
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public AuthenticationController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration.GetSection("JWTConfig");
            _userService = userService;
        }
        #endregion
        #region Methods
        [HttpPost]
        public IActionResult Login(AuthenticationDTO user)
        {
            // check if user exists and credentials are valid
            var result = _userService.IsUserExist(user);
            if (result == null)
            {
                return Unauthorized(new ResponseDTO
                {
                    Status = 401,
                    Message = "Unauthorized",
                    Error = "Incorrect email or password"
                });
            }
            // create jwt access token
            var now = DateTime.UtcNow;
            // jwt claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iat, now.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
                new Claim("Id", Convert.ToString(result.Id)),
            };
            // signing key
            var symmetricKeyAsBase64 = _configuration["SecretKey"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // token options
            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Issuer"],
                audience: _configuration["Audience"],
                claims: claims,
                expires: now.Add(TimeSpan.FromHours(24)),
                signingCredentials: signingCredentials
            );

            // access token
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            // create and return response
            var response = new
            {
                id = result.Id,
                role = result.Role,
                email = result.Email,
                access_token = tokenString,
                expires_in = (int)TimeSpan.FromHours(24).TotalSeconds
            };
            return Ok(response);

        }

        #endregion

    }
}