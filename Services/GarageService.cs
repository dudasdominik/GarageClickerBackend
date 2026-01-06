using GarageClickerBackend.Data;
using GarageClickerBackend.Services.Interfaces;

namespace GarageClickerBackend.Services;

public class GarageService
{
    protected readonly GarageClickerDbContext _context;
    
    public GarageService(GarageClickerDbContext context)
    {
        _context = context;
    }
    
}