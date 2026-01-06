using System.Globalization;
using System.Text.RegularExpressions;
using GarageClickerBackend.Data;
using GarageClickerBackend.Models;
using GarageClickerBackend.Models.DTO;
using GarageClickerBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GarageClickerBackend.Services.Implementation;

public class PlayerItemService(GarageClickerDbContext context) : GarageService(context), IPlayerItemsService
{
    public async Task<List<PlayerItem>> GetAllPlayerItems(Guid playerId)
    {
        return await _context.PlayerItems
            .Where(pi => pi.PlayerId == playerId)
            .Include(pi => pi.Item)
            .ToListAsync();
    }

    public async Task<PlayerItem> GetPlayerItem(Guid playerId, Guid itemId)
    {
        var item = await _context.PlayerItems
            .FirstOrDefaultAsync(pi => pi.PlayerId.Equals(playerId) && pi.ItemId.Equals(itemId));
        if (item == null)
        {
            throw new Exception($"PlayerItem with playerId {playerId} and itemId {itemId} not found");
        } 
        return item;
    }

    public async Task<PlayerItem> AddItemToPlayer(Guid playerId, Guid itemId)
    {
       var player = await _context.Players.FirstOrDefaultAsync(p => p.Id.Equals(playerId));
       var item = await _context.Items.FirstOrDefaultAsync(i => i.Id.Equals(itemId));
       if (player == null || item == null)
       {
           throw new Exception($"PlayerItem with playerId {playerId} and itemId {itemId} not found");
       }
        
        
       var newPlayerItem = new PlayerItem
        {
            Id = Guid.NewGuid(),
            PlayerId = playerId,
            ItemId = itemId,
            Quantity = 1
        };
        _context.PlayerItems.Add(newPlayerItem);
        await _context.SaveChangesAsync();
        return newPlayerItem;
    }

    public async Task<PlayerItem> RemoveItemFromPlayer(Guid playerId, Guid itemId)
    {
        var player = await _context.Players.FirstOrDefaultAsync(p => p.Id.Equals(playerId));
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id.Equals(itemId));
        if (player == null || item == null)
        {
            throw new Exception($"PlayerItem with playerId {playerId} and itemId {itemId} not found");
        }
        
        var playerItem = await _context.PlayerItems
            .FirstOrDefaultAsync(pi => pi.PlayerId.Equals(playerId) && pi.ItemId.Equals(itemId));
        if (playerItem == null)
        {
            throw new Exception($"PlayerItem with playerId {playerId} and itemId {itemId} not found");
        }
        else
        {
            _context.PlayerItems.Remove(playerItem);
            await _context.SaveChangesAsync();
            return playerItem;
        }
    }

    public async Task<PlayerItem> SellOneFromPlayer(Guid playerId, Guid itemId)
    {
        var player = await _context.Players.FirstOrDefaultAsync(p => p.Id.Equals(playerId));
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id.Equals(itemId));
        if (player == null || item == null)
        {
            throw new Exception($"PlayerItem with playerId {playerId} and itemId {itemId} not found");
        }
        
        var itemAtPlayer = await _context.PlayerItems
            .FirstOrDefaultAsync(pi => pi.PlayerId.Equals(playerId) && pi.ItemId.Equals(itemId));
        if (itemAtPlayer == null)
        {
            throw new Exception($"PlayerItem with playerId {playerId} and itemId {itemId} not found");
        }
        if (itemAtPlayer.Quantity <= 0)
        {
                throw new Exception($"PlayerItem with playerId {playerId} and itemId {itemId} has no items to sell");
        }
        itemAtPlayer.Quantity -= 1;
        var price = item.Cost;
        player.Credits += (price / 2);
        await _context.SaveChangesAsync();
        return itemAtPlayer;
    }
    

public async Task<PlayerItem> BuyOneFromPlayer(Guid playerId, Guid itemId)
{
    var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);
    if (player is null) throw new Exception($"Player {playerId} not found");

    var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
    if (item is null) throw new Exception($"Item {itemId} not found");

    var playerItem = await _context.PlayerItems
        .FirstOrDefaultAsync(pi => pi.PlayerId == playerId && pi.ItemId == itemId);

    if (playerItem is null)
    {
        playerItem = new PlayerItem
        {
            Id = Guid.NewGuid(),
            PlayerId = playerId,
            ItemId = itemId,
            Quantity = 0
        };
        _context.PlayerItems.Add(playerItem);
    }

    // Ár számítás: baseCost * 1.15^owned
    var owned = playerItem.Quantity; // vásárlás előtti darabszám
    var baseCost = (decimal)item.Cost;
    var multiplier = (decimal)Math.Pow(1.15, owned);
    var price = (long)Math.Floor(baseCost * multiplier);
    if (price < 1) price = 1;

    if (player.Credits < price)
        throw new Exception("Not enough credits");

    player.Credits -= price;
    playerItem.Quantity += 1;
    
    var cpsDelta = ParseCpsDelta(item.Effect);
    if (cpsDelta > 0m)
    {
        player.ClicksPerSecond += cpsDelta;
    }

    await _context.SaveChangesAsync();
    return playerItem;
}

// --- helper ---
private static decimal ParseCpsDelta(string effect)
{
    if (string.IsNullOrWhiteSpace(effect)) return 0m;
    var match = Regex.Match(effect, @"increase\s+cps\s+by\s+([0-9]+(?:[.,][0-9]+)?)",
        RegexOptions.IgnoreCase);

    if (!match.Success) return 0m;

    var numStr = match.Groups[1].Value.Replace(',', '.');

    return decimal.TryParse(numStr, NumberStyles.Number, CultureInfo.InvariantCulture, out var val)
        ? val
        : 0m;
}


}