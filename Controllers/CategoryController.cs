using e_commerceAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext context;
        public CategoryController(AppDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = context.Categories.ToList();
            return Ok(categories);
        }
    }
}
