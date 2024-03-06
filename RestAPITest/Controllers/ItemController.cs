using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPITest.Data;
using RestAPITest.Data.Models;
using RestAPITest.DataModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateItem(int id , [FromForm] ItemData itemData)
        {
            var item = await _dbcontext.items.FindAsync(id);
            if(item == null)
            {
                return NotFound("Item id not found");
            }
            var checkCategory = await _dbcontext.categories.AnyAsync(x => x.Id == itemData.CategoryId);
            if (!checkCategory)
            {
                return NotFound("categoryId not found");
            }
            if(itemData.Image != null )
            {
                using var stream = new MemoryStream();
                await itemData.Image.CopyToAsync(stream);
                item.Image = stream.ToArray(); 
            }
            item.Name = itemData.Name;
            item.Notes = itemData.Notes;
            item.Price = itemData.Price;
            item.CategoryId = itemData.CategoryId;
            _dbcontext.SaveChanges();
            return Ok(item);
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
