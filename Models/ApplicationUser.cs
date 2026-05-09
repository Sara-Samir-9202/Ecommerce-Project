using Microsoft.AspNetCore.Identity;

namespace e_commerceAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? PaymentDetails { get; set; } //  StripeCustomerId
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = true; //  Admin approval
        public bool IsDeleted { get; set; } = false; // Soft delete
    }
}