namespace GarageClickerBackend.Models.DTO;

public class CreateUserDTO
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public CreateUserDTO(string email, string username, string password)
    {
        Email = email;
        Username = username;
        Password = password;
    }
    
    public Player toPlayer()
    {
        return new Player
        {
            Id = Guid.NewGuid(),
            Name = Username,
            Username = Username,
            PasswordHash = Password,
            Credits = 0,
            TotalClicks = 0,
            ClicksPerSecond = 0,
            CreatedAt = DateTime.UtcNow,
            LastSaveAt = DateTime.UtcNow,
            PlayerItems = new List<PlayerItem>()
        };
    }
   
}