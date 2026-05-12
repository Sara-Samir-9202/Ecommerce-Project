using e_commerceAPI.Data;
using e_commerceAPI.DTO.Products;
using e_commerceAPI.Models;
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
        [HttpPost("Add")]
        public IActionResult AddProduct(ProductDTO prdDTO)
        {
            var category = context.Categories.FirstOrDefault(c => c.Name == prdDTO.CategoryName);
            if (category == null)
                return BadRequest("Invalid category name");
            var product = new Product
            {
                Title = prdDTO.Title,
                Price = prdDTO.Price,
                Description = prdDTO.Description,
                Image = prdDTO.Image,
                Stock = prdDTO.Stock,
                Rate = prdDTO.Rate,
                RatingCount = prdDTO.RatingCount,
                CategoryId = category.Id
            };
            context.Products.Add(product);
            context.SaveChanges();
            return Ok(product);
        }
        [HttpGet("{id}")]
        public IActionResult ProductDetails(int id , ProductDTO productDTO)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            var category = context.Categories.FirstOrDefault(c => c.Name == productDTO.CategoryName);
            if (category == null)
                return BadRequest("Invalid category name");
            if (product == null)
                return NotFound();
            product = new Product
            {
                Id = productDTO.Id,
                Title = productDTO.Title,
                Price = productDTO.Price,
                Description = productDTO.Description,
                Image = productDTO.Image,
                Stock = productDTO.Stock,
                Rate = productDTO.Rate,
                RatingCount = productDTO.RatingCount,
                CategoryId = category.Id
            };
            context.Products.Update(product);
            context.SaveChanges();
            return Ok(productDTO);
        }
        [HttpPut("Edit")]
        public IActionResult Edit(int id)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Title = "Updated Title";
            context.SaveChanges();
            return Ok(product);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            context.Products.Remove(product);
            context.SaveChanges();
            return Ok("Deleted");
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
