using e_commerceAPI.Data;
using e_commerceAPI.DTO.Order;
using e_commerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerceAPI.Services
{
    public class CheckoutService
    {
        private readonly AppDbContext context;

        public CheckoutService(AppDbContext context)
        {
            this.context = context;
        }
        public Order Checkout(CheckoutDto dto)
        {
            var cart = context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.Id == dto.CartId);

            if (cart == null || !cart.CartItems.Any())
                return null;

            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.Now,
               // Status = OrderStatus.Pending
            };

            decimal total = 0;

            foreach (var item in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                };

                total += item.Quantity * item.UnitPrice;

                order.OrderItems.Add(orderItem);
            }

            order.TotalAmount = total;

            context.Orders.Add(order);

            context.CartItems.RemoveRange(cart.CartItems);

            context.SaveChanges();

            return order;
        }
    }
}
