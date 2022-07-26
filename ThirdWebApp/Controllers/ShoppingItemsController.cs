using FirstWebApp.Exceptions;
using FirstWebApp.Mappers;
using FirstWebApp.Models;
using FirstWebApp.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingItemsController : ControllerBase
    {
        private readonly ItemService _service;

        public ShoppingItemsController(ItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingItemDto>>> GetAllItems()
        {
            var items = await _service.GetItems();
            return Ok(items.Select(item => item.ToDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ShoppingItemDto>> GetItem(int id)
        {
            var item = await _service.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult> PostItem([FromBody] CreateShoppingItemRequest newItemRequest)
        {
            // this could well be superfluous (I think I remember that being mentioned, we could turn off automated in Services in Program.cs)
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            ShoppingItem createdItem;
            try
            {
                createdItem = await _service.CreateItem(newItemRequest.ToModel());
            }
            catch (ForeignKeyDoesNotExistException e)
            {
                return BadRequest(e);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return Conflict(e);
            }
            
            return CreatedAtAction(
                nameof(GetItem),
                new {id = createdItem.Id},
                createdItem.ToDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ShoppingItemDto?>> DeleteItem(int id)
        {
            var deletedItem = await _service.DeleteItem(id);
            if (deletedItem == null) return NotFound();
            return Ok(deletedItem.ToDto());
        }
        
        
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<ShoppingItemDto>> PatchItem(int id, JsonPatchDocument<ShoppingItemUpdate> patch)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var patchedItem = await _service.PatchItemProperties(id, patch);
                if (patchedItem == null) return NotFound();
                return Ok(patchedItem.ToDto());
            }
            catch (ForeignKeyDoesNotExistException e)
            {
                return BadRequest();
            }
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutItem(int id, [FromBody]ShoppingItemUpdate update)
        {
            try
            {
                await _service.ReplaceItemProperties(id, update);
            }
            catch (ForeignKeyDoesNotExistException e)
            {
                return BadRequest();
            }
            catch (DbUpdateConcurrencyException e)
            {
                var itemToUpdate = await _service.GetItem(id);
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
        //         var update = await _context.Items.FindAsync(id);
        //         
        //         if (update == null)
        //         {
        //             return NotFound();
        //         }
        //         
        //         itemsToDelete.Add(update);
        //     }
        //     
        //     _context.Items.RemoveRange(itemsToDelete);
        //     await _context.SaveChangesAsync();
        //
        //     return Ok(itemsToDelete);
        // }
        
        
    }
}
