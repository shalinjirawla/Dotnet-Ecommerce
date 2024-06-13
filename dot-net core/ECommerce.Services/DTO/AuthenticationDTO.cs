using System.ComponentModel.DataAnnotations;

namespace ECommerce.Services.DTO
{
    public class AuthenticationDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email address is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
