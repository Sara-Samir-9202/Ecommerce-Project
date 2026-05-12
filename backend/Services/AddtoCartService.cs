using e_commerceAPI.Data;
using e_commerceAPI.Models;

namespace e_commerceAPI.Services
{
    public class AddtoCartService
    {
        private readonly AppDbContext context;
        public AddtoCartService(AppDbContext _context)
        {
            context = _context;
        }
        public ServiceResult AddToCart(string userId, int productId, int quantity)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
                return ServiceResult.Fail("Product not found");

            if (quantity <= 0)
                return ServiceResult.Fail("Invalid quantity");

            if (quantity > product.Stock)
                return ServiceResult.Fail("Not enough stock");

            var cart = context.Carts.FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
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
                    UnitPrice = product.Price
                });
            }
            else
            {
                if (existingItem.Quantity + quantity > product.Stock)
                    return ServiceResult.Fail("Not enough stock");

                existingItem.Quantity += quantity;
            }

            product.Stock -= quantity;

            context.SaveChanges();

            return ServiceResult.Ok($"Added {quantity} of {product.Title} to cart.");
        }
    }
}
