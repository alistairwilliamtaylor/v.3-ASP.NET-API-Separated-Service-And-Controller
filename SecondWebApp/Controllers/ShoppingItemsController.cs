using FirstWebApp.Models;
using FirstWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingItemsController : ControllerBase
    {
        private readonly ItemService _service;

        public ShoppingItemsController(ShoppingContext context)
        {
            
            _service = new ItemService(context);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllItems()
        {
            var items = await _service.GetItems();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetItem(int id)
        {
            var item = await _service.GetItem(id);
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
            
            try
            {
                await _service.CreateItem(newItem);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return Conflict(e);
            }
            
            return CreatedAtAction(
                "GetItem",
                new {id = newItem.Id},
                newItem);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deletedItem = await _service.DeleteItem(id);
            if (deletedItem == null) return NotFound();
            return Ok(deletedItem);
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutItem(int id, [FromBody]ShoppingItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            
            try
            {
                await _service.UpdateItem(item);
            }
            catch (DbUpdateConcurrencyException e)
            {
                var itemToUpdate = await _service.GetItem(item.Id);
                if (itemToUpdate == null)
                {
                    return NotFound();
                }
                else
                {
                    return Conflict(e);
                }
            }

            return NoContent();
        }

        // [HttpPost]
        // [Route("delete-items")]
        // public async Task<ActionResult> DeleteItems([FromBody]Batch batch)
        // {
        //     var itemsToDelete = new List<ShoppingItem>();
        //
        //     foreach (var id in batch.Ids)
        //     {
        //         var item = await _context.Items.FindAsync(id);
        //         
        //         if (item == null)
        //         {
        //             return NotFound();
        //         }
        //         
        //         itemsToDelete.Add(item);
        //     }
        //     
        //     _context.Items.RemoveRange(itemsToDelete);
        //     await _context.SaveChangesAsync();
        //
        //     return Ok(itemsToDelete);
        // }
        
        //
        // [HttpPatch("{id:int}")]
        // public ShoppingItem UpdateItem(int id, JsonPatchDocument<ShoppingItem> updatedValues)
        // {
        //     var itemToUpdate = _itemRepo.GetItem(id);
        //     updatedValues.ApplyTo(itemToUpdate);
        //     return _itemRepo.Replace(itemToUpdate);
        // }
    }
}
