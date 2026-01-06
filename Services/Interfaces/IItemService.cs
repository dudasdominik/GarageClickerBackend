using GarageClickerBackend.Models;

namespace GarageClickerBackend.Services.Interfaces;

public interface IItemService
{
    Task<List<Item>> GetItems();
    Task<Item> GetItemById(Guid id);
    Task<Item> GetItemByName(string name);
}