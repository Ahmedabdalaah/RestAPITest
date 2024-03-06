using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPITest.Data;
using RestAPITest.Data.Models;
using RestAPITest.DataModels;

namespace RestAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _dbcontext;
        public ItemController (AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _dbcontext.items.ToListAsync();
            return Ok(items);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _dbcontext.items.SingleOrDefaultAsync(x => x.Id == id);
            if(item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> AddItem ([FromForm]ItemData data)
        {
            using var stream = new MemoryStream();
            await data.Image.CopyToAsync(stream);
            var items = new Item
            {
                Name = data.Name,
                Notes = data.Notes,
                Price = data.Price,
                CategoryId = data.CategoryId,
                Image = stream.ToArray()
            };
            await _dbcontext.items.AddAsync(items);
            await _dbcontext.SaveChangesAsync();
            return Ok(items);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _dbcontext.items.SingleOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound($"  item id {id} not exists !");
            }
            _dbcontext.items.Remove(item);
            await _dbcontext.SaveChangesAsync();
            return Ok(item);
        }

    }
}
