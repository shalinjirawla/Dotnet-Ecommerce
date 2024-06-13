using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.Models
{
    public class Order
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity {  get; set; }

        [Required(ErrorMessage = "Order date is required")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Total amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Total amount must be a non-negative number")]
        public double TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
