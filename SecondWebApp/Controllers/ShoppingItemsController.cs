using FirstWebApp.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FirstWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingItemsController : ControllerBase
    {
        private readonly ShoppingContext _context;

        public ShoppingItemsController(ShoppingContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllItems()
        {
            var items = await _context.Items.ToArrayAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> PostItem([FromBody] ShoppingItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetItem",
                new {id = newItem.Id},
                newItem);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null) return NotFound();

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPost]
        [Route("delete-items")]
        public async Task<ActionResult> DeleteItems([FromBody]Batch batch)
        {
            var itemsToDelete = new List<ShoppingItem>();

            foreach (var id in batch.Ids)
            {
                var item = await _context.Items.FindAsync(id);
                
                if (item == null)
                {
                    return NotFound();
                }
                
                itemsToDelete.Add(item);
            }
            
            _context.Items.RemoveRange(itemsToDelete);
            await _context.SaveChangesAsync();

            return Ok(itemsToDelete);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutItem(int id, [FromBody]ShoppingItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Items.Any(i => i.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        
        // [HttpPatch("{id:int}")]
        // public ShoppingItem UpdateItem(int id, JsonPatchDocument<ShoppingItem> updatedValues)
        // {
        //     var itemToUpdate = _itemRepo.GetItem(id);
        //     updatedValues.ApplyTo(itemToUpdate);
        //     return _itemRepo.Replace(itemToUpdate);
        // }
    }
}
