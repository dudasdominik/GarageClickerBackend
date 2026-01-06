using GarageClickerBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GarageClickerBackend.Controllers;
[ApiController]
[Route("api/[controller]/")]
public class PlayerItemsController : ControllerBase
{
    private readonly IPlayerItemsService _playerItemsService;
    public PlayerItemsController(IPlayerItemsService playerItemsService)
    {
        _playerItemsService = playerItemsService;
    }
    
    [HttpGet("get/{playerId}/{itemId}")]
    public async Task<IActionResult> GetPlayerItem(Guid playerId, Guid itemId)
    {
        var playerItem = await _playerItemsService.GetPlayerItem(playerId, itemId);
        if (playerItem == null)
        {
            return NotFound();
        }
        return Ok(playerItem);
    }
    
    [HttpGet("all/{playerId}")]
    public async Task<IActionResult> GetPlayerItems(Guid playerId)
    {
        var playerItems = await _playerItemsService.GetAllPlayerItems(playerId);
        if (playerItems == null || playerItems.Count == 0)
        {
            return NotFound();
        }
        return Ok(playerItems);
    }
    
    [HttpPost("add/{playerId}")]
    public async Task<IActionResult> AddPlayerItem(Guid playerId, [FromBody] Guid itemId)
    {
        var addedPlayerItem = await _playerItemsService.AddItemToPlayer(playerId, itemId);
        if (addedPlayerItem == null)
        {
            return BadRequest("Failed to add player item");
        }
        return Ok(addedPlayerItem);
    }
    
    [HttpDelete("delete/{playerId}/{itemId}")]
    public async Task<IActionResult> DeletePlayerItem(Guid playerId, Guid itemId)
    {
        var deleted = await _playerItemsService.RemoveItemFromPlayer(playerId, itemId);
        if (deleted == null)
        {
            return BadRequest("Failed to delete player item");
        }
        return Ok();
    }
    
    [HttpPatch("sell/{playerId}/{itemId}")]
    public async Task<IActionResult> SellPlayerItem(Guid playerId, Guid itemId)
    {
        var soldItem = await _playerItemsService.SellOneFromPlayer(playerId, itemId);
        if (soldItem == null)
        {
            return BadRequest("Failed to sell player item");
        }
        return Ok(soldItem);
    }
    
    [HttpPatch("add/{playerId}/{itemId}")]
    public async Task<IActionResult> AddOnePlayerItem(Guid playerId, Guid itemId)
    {
        var addedItem = await _playerItemsService.BuyOneFromPlayer(playerId, itemId);
        if (addedItem == null)
        {
            return BadRequest("Failed to add player item");
        }
        return Ok(addedItem);
    }
}