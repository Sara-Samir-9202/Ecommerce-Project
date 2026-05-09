using e_commerceAPI.DTO.Auth;
using e_commerceAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace e_commerceAPI.Services
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> UpdateProfileAsync(string userId, UpdateProfileDTO dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            user.FullName = dto.FullName ?? user.FullName;
            user.Address = dto.Address ?? user.Address;
            user.PhoneNumber = dto.PhoneNumber ?? user.PhoneNumber;
            user.PaymentDetails = dto.PaymentDetails ?? user.PaymentDetails;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return _userManager.Users.ToList();
        }

        public async Task<bool> SoftDeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            user.IsDeleted = true;
            user.UserName = $"deleted_{user.Id}";
            user.Email = $"deleted_{user.Id}@deleted.com";
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}