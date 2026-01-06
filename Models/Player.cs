namespace GarageClickerBackend.Models;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public long Credits { get; set; }
    public long TotalClicks { get; set; }
    public decimal ClicksPerSecond { get; set; }
    public DateTime LastSaveAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<PlayerItem> PlayerItems { get; set; } = new List<PlayerItem>();
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    
    
}