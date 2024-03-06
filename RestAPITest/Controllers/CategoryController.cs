using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPITest.Data;
using RestAPITest.Data.Models;

namespace RestAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _dbcontext;
        public CategoryController(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _dbcontext.categories.ToListAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategories(int id)
        {
            var cats = await _dbcontext.categories.SingleOrDefaultAsync(x => x.Id == id);
            if (cats == null)
            {
                return NotFound($"Category Id {id} not exists ");
            }
            return Ok(cats);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(string category)
        {
            Category cat = new() { Name = category };
            await _dbcontext.categories.AddAsync(cat);
            _dbcontext.SaveChanges();
            return Ok(cat);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            var cat = await _dbcontext.categories.SingleOrDefaultAsync(x => x.Id == category.Id);
            if(cat == null)
            {
                return NotFound($"Category {category.Id} is not found");
            }
             cat.Name = category.Name;
            _dbcontext.SaveChanges();
            return Ok(cat);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            var category = await _dbcontext.categories.SingleOrDefaultAsync( x => x.Id == id);
            if(category == null)
            {
                return NotFound($"Category {id} is not found");
            }
            _dbcontext.categories.Remove(category);
            _dbcontext.SaveChanges();
            return Ok(category);
        }
        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateCategoryPatch(
            [FromBody] JsonPatchDocument<Category> category , [FromRoute]int id)
        {
            var cat = await _dbcontext.categories.SingleOrDefaultAsync(x => x.Id == id);
            if (cat == null)
            {
                return NotFound($"Category {id} is not found");
            }
            category.ApplyTo(cat);
            _dbcontext.SaveChangesAsync();
            return Ok(cat);
        }
    }
}
