using ECommerce.Models.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.DTO
{
    public class AddProductDTO
    {
        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(30, ErrorMessage = "Name can not be longer than 30 characters")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "product photo URL is required")]
        public IFormFile ProductPhotoUrl { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive integer")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public double Price { get; set; }
        public List<int> ProductCategories { get; set; }
    }
}
