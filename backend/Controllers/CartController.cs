using e_commerceAPI.Data;
using e_commerceAPI.DTO;
using e_commerceAPI.Models;
using e_commerceAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly AddtoCartService cartService;
        public CartController(AppDbContext _context, AddtoCartService _cartService)
        {
            context = _context;
            cartService = _cartService;
        }
        [HttpPost("Add")]
        public IActionResult AddToCart(AddToCartDTO dto)
        {
            var result = cartService.AddToCart(dto.UserId, dto.ProductId, dto.Quantity);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("{cartId}/{productId}")]
        public IActionResult Delete(int cartId, int productId)
        {
            var item = context.CartItems.FirstOrDefault(c => c.CartId == cartId && c.ProductId == productId);
            if (item == null)
                return NotFound("Item not found in cart");
            context.CartItems.Remove(item);
            context.SaveChanges();
            return Ok("Deleted");

        }
        [HttpPut("update")]
        public IActionResult UpdateQuantity(int cartid, int productid, int quantity)
        {
            var item = context.CartItems.FirstOrDefault(c => c.CartId == cartid && c.ProductId == productid);
            if (item == null)
                return NotFound("Item not found in cart");
            if (quantity <= 0)
                return BadRequest("Quantity must be greater than zero");
            item.Quantity = quantity;
            context.SaveChanges();
            return Ok("Updated");
        }
        [HttpGet("{cartId}")]
        public IActionResult GetCart(int cartId)
        {
            var cart = context.Carts.Include(c=> c.CartItems).ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
                return NotFound("Cart not found");
              var total = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
            return Ok(new
            {
                cart.Id,
                Items = cart.CartItems.Select(ci => new
                {
                    ci.ProductId,
                    ProductName = ci.Product.Title,
                    ci.Quantity,
                    ci.UnitPrice,
                    Total = ci.Quantity * ci.UnitPrice
                }),
                Total = total
            });
        }
        [HttpDelete("Clear/{cartId}")]
        public IActionResult ClearCart(int cartId)
        {
            var cart = context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
                return NotFound("Cart not found");
            context.CartItems.RemoveRange(cart.CartItems);
            context.SaveChanges();
            return Ok("Cart cleared");
        }
    }
}
