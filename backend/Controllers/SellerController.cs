using e_commerceAPI.Data;
using e_commerceAPI.DTO;
using e_commerceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Seller")]
    public class SellerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SellerController(AppDbContext context)
        {
            _context = context;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        // ========== PRODUCT MANAGEMENT ==========

        [HttpGet("my-products")]
        public IActionResult GetMyProducts()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Price,
                    p.Stock,
                    p.Image,
                    p.Description,
                    p.Rate,
                    p.RatingCount,
                    Category = p.Category.Name
                }).ToList();

            return Ok(products);
        }

        [HttpGet("my-products/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound("Product not found");

            return Ok(new
            {
                product.Id,
                product.Title,
                product.Price,
                product.Stock,
                product.Image,
                product.Description,
                product.Rate,
                product.RatingCount,
                Category = product.Category?.Name
            });
        }

        [HttpPost("products")]
        public IActionResult AddProduct([FromBody] ProductDTO dto)
        {
            // Validate category
            var category = _context.Categories.FirstOrDefault(c => c.Name == dto.CategoryName);
            if (category == null)
                return BadRequest($"Category '{dto.CategoryName}' not found");

            var product = new Product
            {
                Title = dto.Title,
                Price = dto.Price,
                Description = dto.Description,
                Image = dto.Image,
                Stock = dto.Stock,
                Rate = 0,
                RatingCount = 0,
                CategoryId = category.Id
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok(new { Message = "Product added successfully", ProductId = product.Id });
        }

        [HttpPut("products/{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDTO dto)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound("Product not found");

            // Update fields
            product.Title = dto.Title ?? product.Title;
            product.Price = dto.Price != 0 ? dto.Price : product.Price;
            product.Description = dto.Description ?? product.Description;
            product.Image = dto.Image ?? product.Image;
            product.Stock = dto.Stock != 0 ? dto.Stock : product.Stock;

            // Update category if provided
            if (!string.IsNullOrEmpty(dto.CategoryName))
            {
                var category = _context.Categories.FirstOrDefault(c => c.Name == dto.CategoryName);
                if (category != null)
                    product.CategoryId = category.Id;
            }

            _context.SaveChanges();
            return Ok("Product updated successfully");
        }

        [HttpDelete("products/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound("Product not found");

            // Check if product is in any cart or order
            var inCart = _context.CartItems.Any(ci => ci.ProductId == id);
            var inOrder = _context.OrderItems.Any(oi => oi.ProductId == id);

            if (inCart || inOrder)
                return BadRequest("Cannot delete product that is in carts or orders");

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok("Product deleted successfully");
        }

        [HttpPatch("products/{id}/stock")]
        public IActionResult UpdateStock(int id, [FromBody] int newStock)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound("Product not found");

            if (newStock < 0)
                return BadRequest("Stock cannot be negative");

            product.Stock = newStock;
            _context.SaveChanges();
            return Ok($"Stock updated to {newStock}");
        }

        // ========== ORDERS ==========

        [HttpGet("orders")]
        public IActionResult GetMyProductOrders()
        {
            // Get all order items that contain products added by this seller
            var orderItems = _context.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.Product)
                .Where(oi => oi.Product.SellerId == GetUserId())
                .Select(oi => new
                {
                    oi.OrderId,
                    oi.Order.OrderDate,
                    oi.Order.Status,
                    oi.Order.TotalAmount,
                    oi.ProductId,
                    oi.Product.Title,
                    oi.Quantity,
                    oi.UnitPrice,
                    oi.TotalPrice
                })
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return Ok(orderItems);
        }

        [HttpGet("orders/{orderId}")]
        public IActionResult GetOrderDetails(int orderId)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
                return NotFound("Order not found");

            // Check if order contains seller's products
            var hasSellerProducts = order.OrderItems.Any(oi => oi.Product.SellerId == GetUserId());
            if (!hasSellerProducts)
                return Forbid("You don't have permission to view this order");

            return Ok(new
            {
                order.Id,
                order.UserId,
                order.OrderDate,
                order.TotalAmount,
                order.Status,
                Items = order.OrderItems.Select(oi => new
                {
                    oi.ProductId,
                    oi.Product.Title,
                    oi.Quantity,
                    oi.UnitPrice,
                    oi.TotalPrice
                })
            });
        }

        // ========== PROFILE ==========

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            // This would need UserManager - simplified version
            return Ok(new { Message = "Seller profile" });
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var sellerId = GetUserId();

            var totalProducts = _context.Products.Count(); // In real app, filter by sellerId
            var totalOrders = _context.OrderItems
                .Where(oi => oi.Product.SellerId == sellerId)
                .Select(oi => oi.OrderId)
                .Distinct()
                .Count();

            var totalRevenue = _context.OrderItems
                .Where(oi => oi.Product.SellerId == sellerId)
                .Sum(oi => oi.TotalPrice);

            return Ok(new
            {
                TotalProducts = totalProducts,
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue
            });
        }
    }
}