using e_commerceAPI.Data;
using e_commerceAPI.Models;
using e_commerceAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly CartService cartService;
        public CartController(AppDbContext _context , CartService _cartService)
        {
            context = _context;
            cartService = _cartService;
        }
        [HttpPost("Add")]
        public IActionResult AddToCart(string userId , int productId, int quantity)
        {
            var result = cartService.AddToCart(userId, productId, quantity);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
