namespace e_commerceAPI.DTO.Auth
{
    public class UpdateProfileDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? PaymentDetails { get; set; }
    }
}