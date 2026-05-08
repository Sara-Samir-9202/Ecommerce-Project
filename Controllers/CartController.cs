using e_commerceAPI.Data;
using e_commerceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext context;
        public CartController(AppDbContext _context)
        {
            context = _context;
        }
        [HttpPost("Add")]
        public IActionResult AddToCart(string userId , int productId, int quantity)
        {
            var cartuser = context.Carts.FirstOrDefault(u => u.SessionId == userId);
            if (cartuser == null)
            {
                cartuser = new Cart
                {
                    SessionId = userId,
                    CartItems = new List<CartItem>(),
                    CreatedAt = DateTime.Now
                };
                context.Carts.Add(cartuser);
                context.SaveChanges();
            }
            var product = context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return NotFound("Product not found");
            if (quantity <= 0 || quantity > product.Stock)
            {
                return BadRequest("Invalid quantity.");
            }
            var existingCartItem = context.CartItems.FirstOrDefault(ci => ci.CartId == cartuser.Id && ci.ProductId == productId);
            if (existingCartItem == null)
            {
                var cartItem = new CartItem
                {
                    CartId = cartuser.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price
                };
                context.CartItems.Add(cartItem);
            }
            else
            {
                existingCartItem.Quantity += quantity;
                context.CartItems.Update(existingCartItem);
            }
            product.Stock -= quantity;

            context.Products.Update(product); 

            context.SaveChanges();
            return Ok($"Added {quantity} of {product.Title} to cart.");
        }
    }
}
