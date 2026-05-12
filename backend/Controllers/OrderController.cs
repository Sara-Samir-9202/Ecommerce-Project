using e_commerceAPI.Data;
using e_commerceAPI.DTO;
using e_commerceAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly CheckoutService checkoutService;
        public OrderController(AppDbContext _context , CheckoutService _checkoutService)
        {
            context = _context;
            checkoutService = _checkoutService;
        }
        [HttpPost("checkout")]
        public IActionResult Checkout([FromBody] CheckoutDto dto)
        {
            var result = checkoutService.Checkout(dto);

            if (result == null)
                return BadRequest("Cart is empty");

            return Ok(result);
        }

    }
}
