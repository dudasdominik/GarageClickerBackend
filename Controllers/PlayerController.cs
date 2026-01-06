using GarageClickerBackend.Models.DTO;
using GarageClickerBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GarageClickerBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly IItemService _itemService;
    public PlayerController(IPlayerService playerService, IItemService itemService)
    {
        _playerService = playerService;
        _itemService = itemService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlayerById(Guid id)
    {
        var player = await _playerService.GetPlayerById(id);
        if (player == null)
        {
            return NotFound();
        }
        return Ok(player);
    }
    
    [HttpGet("items/{playerId}")]
    public async Task<IActionResult> GetPlayerItem(Guid playerId)
    {
        var playerItem = await _playerService.GetPlayerItem(playerId);
        if (playerItem == null)
        {
            return NotFound();
        }
        return Ok(playerItem);
    }
    
    [HttpGet("items")]
    public async Task<IActionResult> GetPlayerItems()
    {
        var items = await _playerService.GetPlayerItems();
        if (items == null || items.Count == 0)
        {
            return NotFound();
        }
        return Ok(items);
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddPlayer([FromBody] CreateUserDTO player)
    {
        if (player == null)
        {
            return BadRequest("Player cannot be null");
        }
        
        var addedPlayer = await _playerService.AddPlayer(player);
        var allItems = await _itemService.GetItems();
        await _playerService.AddAllItemsToPlayer(addedPlayer.Id, allItems);
        var fullPlayer = await _playerService.GetPlayerWithItems(addedPlayer.Id);
        return Ok(fullPlayer);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginPlayer([FromBody] LoginDetailsDTO loginDetails)
    {
        if (loginDetails == null)
        {
            return BadRequest("Login details cannot be null");
        }
        
        var player = await _playerService.LoginPlayer(loginDetails);
        if (player == null)
        {
            return Unauthorized("Invalid username or password");
        }
        var fullPlayer = await _playerService.GetPlayerWithItems(player.Id);

        return Ok(fullPlayer);
    }
    
    [HttpPost("save")]
    public async Task<IActionResult> SavePlayer([FromBody] SavePlayerRequestDTO request)
    {
        if (request == null)
        {
            return BadRequest("Save request cannot be null");
        }
        
        var savedPlayer = await _playerService.SavePlayer(request);
        if (savedPlayer == null)
        {
            return BadRequest("Failed to save player");
        }
        var fullPlayer = await _playerService.GetPlayerWithItems(savedPlayer.Id);

        return Ok(fullPlayer);
    }
}