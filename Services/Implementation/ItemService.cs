using GarageClickerBackend.Data;
using GarageClickerBackend.Models;
using GarageClickerBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GarageClickerBackend.Services.Implementation;

public class ItemService : GarageService, IItemService
{
    public ItemService(GarageClickerDbContext context) : base(context)
    {
    }

    public async Task<List<Item>> GetItems()
    {
        var items = await _context.Items.ToListAsync();
        return items;
    }

    public async Task<Item> GetItemById(Guid id)
    {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id.Equals(id));
        if (item == null)
        {
            throw new Exception($"Item with id {id} not found");
        }

        return item;
    }

    public async Task<Item> GetItemByName(string name)
    {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Name.Equals(name));
        if (item == null)
        {
            throw new Exception($"Item with name {name} not found");
        }
        return item;
    }
}