using e_commerceAPI.Data;
using e_commerceAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext context;
        public ProductsController(AppDbContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = context.Products.ToList();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult ProductDetails(int id)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var prdDTO = new ProductDTO
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                Image = product.Image,
                Stock = product.Stock,
                Rate = product.Rate,
                RatingCount = product.RatingCount,
                CategoryName = context.Categories.FirstOrDefault(c => c.Id == product.CategoryId)?.Name,
            };
            return Ok(prdDTO);
        }
        [HttpGet]
        [Route("category/{categoryId}")]
        public IActionResult GetProductsByCategory(int categoryId)
        {
            var products = context.Products.Where(p => p.CategoryId == categoryId).ToList();
            if (products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpGet("search")]
        public IActionResult SearhByName(string name)
        {
            var prd = context.Products.Where(p => p.Title.Contains(name)).ToList();
            if (prd.Count == 0)
            {
                return NotFound();
            }
            return Ok(prd);
        }

        [HttpGet("byPrice")]
        public IActionResult ByPrice(decimal price)
        {
            var prd = context.Products.Where(p => p.Price <= price).ToList();
            if (prd.Count == 0)
            {
                return NotFound();
            }
            return Ok(prd);
        }
    }
}
