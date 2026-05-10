namespace e_commerceAPI.DTO.Order
{
    public enum PaymentMethod
    {
        CreditCard,
        PayPal,
        CashOnDelivery
    }
    public class CheckoutDto
    {
        public int CartId { get; set; }

        public string UserId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public ShippingAddressDTO ShippingAddress { get; set; }
    }
}
