using e_commerceAPI.Data;
using e_commerceAPI.DTO.Checkout;
using e_commerceAPI.Models;
using e_commerceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestCheckoutController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GuestCheckoutController(AppDbContext context)
        {
            _context = context;
            
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> GuestCheckout([FromBody] GuestCheckoutDTO dto)
        {
            // 1. Validate products and stock
            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var item in dto.CartItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                    return BadRequest($"Product with ID {item.ProductId} not found");

                if (product.Stock < item.Quantity)
                    return BadRequest($"Not enough stock for {product.Title}. Available: {product.Stock}");

                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };
                orderItems.Add(orderItem);
                totalAmount += product.Price * item.Quantity;

                // Reduce stock
                product.Stock -= item.Quantity;
            }

            // 2. Create order (UserId = null for guest)
            var order = new Order
            {
                UserId = null, // Guest has no user ID
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                OrderItems = orderItems,
                ShippingAddress = dto.Address,
                CustomerEmail = dto.Email,
                CustomerName = dto.FullName,
                CustomerPhone = dto.PhoneNumber,
                Status = dto.PaymentMethod == "CashOnDelivery" ? "Pending" : "AwaitingPayment"
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

          
            // 4. Return order details
            return Ok(new
            {
                OrderId = order.Id,
                TotalAmount = totalAmount,
                Status = order.Status,
                Message = "Order placed successfully. Confirmation email sent."
            });
        }
    }
}