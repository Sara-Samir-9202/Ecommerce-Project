using e_commerceAPI.Data;
using e_commerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayPalController : ControllerBase
    {
        private readonly PayPalService _payPalService;
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public PayPalController(PayPalService payPalService, AppDbContext context, IConfiguration config)
        {
            _payPalService = payPalService;
            _context = context;
            _config = config;
        }

        [Authorize]
        [HttpPost("create-order/{orderId}")]
        public async Task<IActionResult> CreatePayPalOrder(int orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null || order.UserId != userId)
                return NotFound("Order not found");

            var returnUrl = $"{Request.Scheme}://{Request.Host}/api/paypal/success?orderId={orderId}";
            var cancelUrl = $"{Request.Scheme}://{Request.Host}/api/paypal/cancel";

            var approvalUrl = await _payPalService.CreateOrder(order.TotalAmount, orderId, returnUrl, cancelUrl);

            if (approvalUrl == null)
                return BadRequest("PayPal is not configured or payment failed");

            return Ok(new { ApprovalUrl = approvalUrl });
        }

        [HttpGet("success")]
        public async Task<IActionResult> PaymentSuccess(int orderId, string token)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return NotFound();

            order.Status = "Paid";
            await _context.SaveChangesAsync();

            return Redirect($"{_config["FrontendUrl"]}/payment-success?orderId={orderId}");
        }

        [HttpGet("cancel")]
        public IActionResult PaymentCancel()
        {
            return Redirect($"{_config["FrontendUrl"]}/payment-cancel");
        }
    }
}