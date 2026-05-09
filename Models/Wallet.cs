using System;

namespace e_commerceAPI.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public decimal Balance { get; set; } = 0;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class WalletTransaction
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty; // Deposit, Payment, Refund
        public int? OrderId { get; set; }
        public string? TransactionId { get; set; }
        public string Status { get; set; } = "Completed";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}