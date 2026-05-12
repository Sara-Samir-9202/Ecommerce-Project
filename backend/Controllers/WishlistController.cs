using e_commerceAPI.Data;
using e_commerceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishlistController : ControllerBase
    {
        private readonly AppDbContext _context;
        public WishlistController(AppDbContext context) => _context = context;
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet]
        public IActionResult Get() =>
            Ok(_context.WishlistItems.Include(w => w.Product).Where(w => w.UserId == GetUserId())
                .Select(w => new { w.ProductId, w.Product.Title, w.Product.Price, w.Product.Image, w.AddedAt }));

        [HttpPost("{productId}")]
        public IActionResult Add(int productId)
        {
            if (!_context.Products.Any(p => p.Id == productId)) return NotFound("Product not found");
            if (_context.WishlistItems.Any(w => w.UserId == GetUserId() && w.ProductId == productId))
                return BadRequest("Already in wishlist");

            _context.WishlistItems.Add(new WishlistItem { UserId = GetUserId(), ProductId = productId });
            _context.SaveChanges();
            return Ok("Added to wishlist");
        }

        [HttpDelete("{productId}")]
        public IActionResult Remove(int productId)
        {
            var item = _context.WishlistItems.FirstOrDefault(w => w.UserId == GetUserId() && w.ProductId == productId);
            if (item == null) return NotFound();
            _context.WishlistItems.Remove(item);
            _context.SaveChanges();
            return Ok("Removed from wishlist");
        }
    }
}