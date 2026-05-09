using System;

namespace e_commerceAPI.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty; // "Stripe", "PayPal", "CashOnDelivery", "Wallet"
        public string? TransactionId { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Completed, Failed
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }
}