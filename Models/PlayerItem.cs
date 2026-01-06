namespace GarageClickerBackend.Models;

public class PlayerItem
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public Player Player { get; set; }
    public Guid ItemId { get; set; }
    public Item Item { get; set; }
    public int Quantity { get; set; }
}