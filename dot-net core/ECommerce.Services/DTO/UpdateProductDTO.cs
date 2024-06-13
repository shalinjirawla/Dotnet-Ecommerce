namespace ECommerce.Services.DTO
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductPhotoUrl { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public List<int> ProductCategories { get; set; }

    }
}
