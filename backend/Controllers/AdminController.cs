using e_commerceAPI.Data;
using e_commerceAPI.DTO.Admin;
using e_commerceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ========== USER MANAGEMENT ==========
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var result = new List<object>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new
                {
                    user.Id,
                    user.FullName,
                    user.Email,
                    user.PhoneNumber,
                    user.IsApproved,
                    user.IsDeleted,
                    Roles = roles
                });
            }
            return Ok(result);
        }

        [HttpPut("users/approve")]
        public async Task<IActionResult> ApproveUser(UserManageDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null) return NotFound();

            user.IsApproved = dto.IsApproved;
            await _userManager.UpdateAsync(user);
            return Ok($"User {(dto.IsApproved ? "approved" : "restricted")}");
        }

        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> SoftDeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            user.IsDeleted = true;
            user.Email = $"deleted_{user.Id}@deleted.com";
            await _userManager.UpdateAsync(user);
            return Ok("User soft deleted");
        }

        // ========== SELLER MANAGEMENT (NEW) ==========
        [HttpGet("pending-sellers")]
        public async Task<IActionResult> GetPendingSellers()
        {
            var pendingSellers = await _userManager.Users
                .Where(u => !u.IsApproved && u.Email != null)
                .Select(u => new { u.Id, u.FullName, u.Email, u.PhoneNumber, u.Address })
                .ToListAsync();

            return Ok(pendingSellers);
        }

        [HttpPut("sellers/{sellerId}/approve")]
        public async Task<IActionResult> ApproveSeller(string sellerId)
        {
            var seller = await _userManager.FindByIdAsync(sellerId);
            if (seller == null)
                return NotFound("Seller not found");

            seller.IsApproved = true;
            await _userManager.UpdateAsync(seller);

            // Ensure seller has Seller role
            if (!await _userManager.IsInRoleAsync(seller, "Seller"))
                await _userManager.AddToRoleAsync(seller, "Seller");

            return Ok($"Seller {seller.Email} approved successfully");
        }

        [HttpPut("sellers/{sellerId}/reject")]
        public async Task<IActionResult> RejectSeller(string sellerId)
        {
            var seller = await _userManager.FindByIdAsync(sellerId);
            if (seller == null)
                return NotFound("Seller not found");

            seller.IsApproved = false;
            await _userManager.UpdateAsync(seller);

            return Ok($"Seller {seller.Email} rejected");
        }

        [HttpGet("sellers")]
        public async Task<IActionResult> GetAllSellers()
        {
            var sellers = await _userManager.GetUsersInRoleAsync("Seller");
            var result = sellers.Select(s => new
            {
                s.Id,
                s.FullName,
                s.Email,
                s.PhoneNumber,
                s.IsApproved,
                s.Address
            }).ToList();

            return Ok(result);
        }

        // ========== BANNER MANAGEMENT ==========
        [HttpGet("banners")]
        public IActionResult GetBanners() => Ok(_context.Banners.OrderBy(b => b.DisplayOrder).ToList());

        [HttpPost("banners")]
        public IActionResult CreateBanner(BannerDTO dto)
        {
            var banner = new Banner
            {
                Title = dto.Title,
                ImageUrl = dto.ImageUrl,
                Link = dto.Link,
                IsActive = dto.IsActive,
                DisplayOrder = dto.DisplayOrder
            };
            _context.Banners.Add(banner);
            _context.SaveChanges();
            return Ok(banner);
        }

        [HttpPut("banners/{id}")]
        public IActionResult UpdateBanner(int id, BannerDTO dto)
        {
            var banner = _context.Banners.Find(id);
            if (banner == null) return NotFound();

            banner.Title = dto.Title;
            banner.ImageUrl = dto.ImageUrl;
            banner.Link = dto.Link;
            banner.IsActive = dto.IsActive;
            banner.DisplayOrder = dto.DisplayOrder;
            _context.SaveChanges();
            return Ok(banner);
        }

        [HttpDelete("banners/{id}")]
        public IActionResult DeleteBanner(int id)
        {
            var banner = _context.Banners.Find(id);
            if (banner == null) return NotFound();
            _context.Banners.Remove(banner);
            _context.SaveChanges();
            return Ok("Banner deleted");
        }

        // ========== DASHBOARD STATS ==========
        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            return Ok(new
            {
                TotalUsers = _userManager.Users.Count(),
                TotalOrders = _context.Orders.Count(),
                TotalProducts = _context.Products.Count(),
                TotalRevenue = _context.Orders.Any() ? _context.Orders.Sum(o => o.TotalAmount) : 0,
                PendingSellers = _userManager.Users.Count(u => !u.IsApproved && u.Email != null)
            });
        }

        // ========== PRODUCT MANAGEMENT (Admin override) ==========
        [HttpGet("products")]
        public IActionResult GetAllProducts()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Price,
                    p.Stock,
                    p.Image,
                    p.Rate,
                    Category = p.Category != null ? p.Category.Name : null
                }).ToList();

            return Ok(products);
        }

        [HttpDelete("products/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok("Product deleted by admin");
        }
    }
}