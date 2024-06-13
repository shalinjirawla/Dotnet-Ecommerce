using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Category is required")]
        [MaxLength(30, ErrorMessage = "Category can not be longer than 30 characters")]
        public string CategoryName { get; set; }
    }
}
