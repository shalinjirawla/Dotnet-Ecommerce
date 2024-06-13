namespace ECommerce.Services.DTO
{
    public class AddCartItemDTO
    {
        public int UserId { get; set; }
        public int ProductId {  get; set; }
        public int Quantity {  get; set; }
    }
}
