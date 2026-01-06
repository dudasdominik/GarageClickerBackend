using GarageClickerBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GarageClickerBackend.Controllers;
[ApiController]
[Route("api/[controller]/")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;
    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }
    
    [HttpGet("all") ]
    public async Task<IActionResult> GetAllItems()
    {
        var items = await _itemService.GetItems();

        if (items == null || items.Count == 0)
        {
            return NotFound();
        }

        return Ok(items);
    }
    
    [HttpGet("itembyid/{itemId}")]
    public async Task<IActionResult> GetItemById(Guid itemId)
    {
        var item = await _itemService.GetItemById(itemId);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }
    
    [HttpGet("itembyname/{name}")]
    public async Task<IActionResult> GetItemByName(string name)
    {
        var item = await _itemService.GetItemByName(name);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }
}
