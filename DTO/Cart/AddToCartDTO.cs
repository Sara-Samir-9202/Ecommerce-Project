namespace e_commerceAPI.DTO.Cart
{
    public class AddToCartDTO
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
