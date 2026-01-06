using GarageClickerBackend.Models;

namespace GarageClickerBackend.Services.Interfaces;

public interface IPlayerItemsService
{
    Task<List<PlayerItem>> GetAllPlayerItems(Guid playerId);
    Task<PlayerItem> GetPlayerItem(Guid playerId, Guid itemId);
    Task<PlayerItem> AddItemToPlayer(Guid playerId, Guid itemId);
    Task<PlayerItem> RemoveItemFromPlayer(Guid playerId, Guid itemId);
    Task<PlayerItem> SellOneFromPlayer(Guid playerId, Guid itemId);
    Task<PlayerItem> BuyOneFromPlayer(Guid playerId, Guid itemId);
}