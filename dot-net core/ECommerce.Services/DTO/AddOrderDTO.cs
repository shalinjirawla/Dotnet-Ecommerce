using System.ComponentModel.DataAnnotations;

namespace ECommerce.Services.DTO
{
    public class AddOrderDTO
    {
        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity {  get; set; }

        [Required(ErrorMessage = "Total amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Total amount must be a non-negative number")]
        public double TotalAmount { get; set; }
    }
}
