using System.ComponentModel.DataAnnotations;

namespace e_commerceAPI.DTO.Auth
{
    public class RegisterDTO
    {
        [Required] public string FullName { get; set; } = string.Empty;
        [Required][EmailAddress] public string Email { get; set; } = string.Empty;
        [Required][Phone] public string PhoneNumber { get; set; } = string.Empty;
        [Required][MinLength(6)] public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        [Required] public string Role { get; set; } = string.Empty; // Customer, Seller
    }
}