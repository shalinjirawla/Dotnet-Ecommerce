using System.ComponentModel.DataAnnotations;

namespace ECommerce.Services.DTO
{
    public class AddUserDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(30, ErrorMessage = "Name can not be longer than 30 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email address is invalid")]
        [MaxLength(64, ErrorMessage = "Email address can not be longer than 64 characters")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(256)]
        public string Password { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must contain 10 digits only")]
        [MaxLength(10)]
        public string MobileNumber { get; set; }
        public string Role { get; set; }
    }
}
