namespace e_commerceAPI.DTO.Payment
{
    public class PaymentIntentDTO
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty; // Stripe, PayPal
        public string? SuccessUrl { get; set; }
        public string? CancelUrl { get; set; }
    }
}