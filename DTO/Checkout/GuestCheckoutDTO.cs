using System.ComponentModel.DataAnnotations;

namespace e_commerceAPI.DTO.Checkout
{
    public class GuestCheckoutDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        public List<GuestCartItemDTO> CartItems { get; set; } = new();

        public string PaymentMethod { get; set; } = "CashOnDelivery"; // CashOnDelivery, PayPal
    }

    public class GuestCartItemDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 999)]
        public int Quantity { get; set; }
    }
}