namespace GarageClickerBackend.Models;

public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Cost { get; set; }
    public string Effect { get; set; }
    public string Type { get; set; } 
}