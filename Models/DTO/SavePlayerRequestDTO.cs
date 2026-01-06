namespace GarageClickerBackend.Models.DTO;

public class SavePlayerRequestDTO
{
    public Guid PlayerId { get; set; }
    public long ClientCredits { get; set; }
    public int ClientTotalClicks { get; set; }
}