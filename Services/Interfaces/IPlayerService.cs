using GarageClickerBackend.Models;
using GarageClickerBackend.Models.DTO;

namespace GarageClickerBackend.Services.Interfaces;

public interface IPlayerService
{
    Task<PlayerItem> GetPlayerItem(Guid playerId);
    Task<List<PlayerItem>> GetPlayerItems();
    Task<Player> GetPlayerById(Guid id);
    Task<Player> AddPlayer(CreateUserDTO player);
    Task<Player> LoginPlayer(LoginDetailsDTO loginDetails);
    Task<Player> SavePlayer(SavePlayerRequestDTO request);
    Task AddAllItemsToPlayer(Guid playerId, IEnumerable<Item> items);
    Task<Player> GetPlayerWithItems(Guid playerId);
}