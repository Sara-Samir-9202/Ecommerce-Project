namespace e_commerceAPI.DTO.Payment
{
    public class PaymentResponseDTO
    {
        public string ClientSecret { get; set; } = string.Empty; //  Stripe
        public string? RedirectUrl { get; set; } // PayPal
        public bool RequiresAction { get; set; }
    }
}