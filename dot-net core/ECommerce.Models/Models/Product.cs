using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Models
{
    public class Product
    {
        [Key] 
        public int Id { get; set; }
        public string ProductName {  get; set; }
        public string ProductPhotoUrl { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int TimesSold { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
