using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingItemsController : ControllerBase
    {
        private IItemRepository _itemRepo;
        
        public ShoppingItemsController(IItemRepository repository)
        {
            _itemRepo = repository;
        }
        
        [HttpGet]
        public IEnumerable<ShoppingItem> GetCollection()
        {
            return _itemRepo.Query();
        }

        [HttpGet("{id:int}")]
        public ShoppingItem GetById(int id)
        {
            return _itemRepo.GetById(id);
        }

        [HttpPost]
        public ShoppingItem PostItem(ShoppingItem newItem)
        {
            return _itemRepo.Add(newItem);
        }

        [HttpDelete("{id:int}")]
        public ShoppingItem DeleteItem(int id)
        {
            return _itemRepo.Remove(id);
        }

        [HttpPut("{id:int}")]
        public ShoppingItem ReplaceItem(int id, ShoppingItem updatedItem)
        {
            return _itemRepo.Replace(updatedItem);
        }

        [HttpPatch("{id:int}")]
        public ShoppingItem UpdateItem(int id, JsonPatchDocument<ShoppingItem> updatedValues)
        {
            var itemToUpdate = _itemRepo.GetById(id);
            updatedValues.ApplyTo(itemToUpdate);
            return _itemRepo.Replace(itemToUpdate);
        }
    }
}
