using e_commerceAPI.Data;
using e_commerceAPI.Models;

namespace e_commerceAPI.Services
{
    public class CartService
    {
        private readonly AppDbContext context;
        public CartService(AppDbContext _context)
        {
            context = _context;
        }
        public ServiceResult AddToCart(string userId, int productId, int quantity)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
                return ServiceResult.Fail("Product not found");

            if (quantity <= 0 || quantity > product.Stock)
                return ServiceResult.Fail("Invalid quantity");

            var cart = context.Carts.FirstOrDefault(c => c.SessionId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    SessionId = userId,
                    CreatedAt = DateTime.Now,
                    CartItems = new List<CartItem>()
                };

                context.Carts.Add(cart);
                context.SaveChanges();
            }

            var existingItem = context.CartItems
                .FirstOrDefault(ci => ci.CartId == cart.Id && ci.ProductId == productId);

            if (existingItem == null)
            {
                context.CartItems.Add(new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price
                });
            }
            else
            {
                existingItem.Quantity += quantity;
            }

            product.Stock -= quantity;

            context.SaveChanges();

            return ServiceResult.Ok($"Added {quantity} of {product.Title} to cart.");
        }
    }
}
