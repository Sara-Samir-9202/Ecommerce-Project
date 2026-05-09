using e_commerceAPI.Data;
using e_commerceAPI.DTO.Payment;
using e_commerceAPI.Models;
using e_commerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly AppDbContext _context;

        public PaymentController(PaymentService paymentService, AppDbContext context)
        {
            _paymentService = paymentService;
            _context = context;
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent(PaymentIntentDTO dto)
        {
            var order = _context.Orders.Find(dto.OrderId);
            if (order == null) return NotFound("Order not found");

            var successUrl = dto.SuccessUrl ?? $"{Request.Scheme}://{Request.Host}/api/payment/success?orderId={order.Id}";
            var cancelUrl = dto.CancelUrl ?? $"{Request.Scheme}://{Request.Host}/api/payment/cancel";

            var result = await _paymentService.CreateStripePaymentIntent(order.Id, order.TotalAmount, successUrl, cancelUrl);
            return Ok(result);
        }

        [HttpGet("success")]
        public async Task<IActionResult> PaymentSuccess(int orderId, string sessionId)
        {
            var verified = await _paymentService.VerifyPayment(sessionId);
            if (!verified) return BadRequest("Payment verification failed");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var payment = new Payment
            {
                OrderId = orderId,
                UserId = userId,
                Amount = _context.Orders.Find(orderId)?.TotalAmount ?? 0,
                PaymentMethod = "Stripe",
                TransactionId = sessionId,
                Status = "Completed"
            };
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok("Payment successful!");
        }
    }
}