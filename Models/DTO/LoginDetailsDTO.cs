namespace GarageClickerBackend.Models.DTO;

public class LoginDetailsDTO
{
    public string Username { get; set; }
    public string Password { get; set; }
    public LoginDetailsDTO(string username, string password)
    {
        Username = username;
        Password = password;
    }
    
    public LoginDetailsDTO() { }
}