namespace ECommerce.Services.DTO
{
    public class UpdateProductRatingDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
    }
}
