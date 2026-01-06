using GarageClickerBackend.Data;
using GarageClickerBackend.Models;
using GarageClickerBackend.Models.DTO;
using GarageClickerBackend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GarageClickerBackend.Services.Implementation;

public class PlayerService : GarageService, IPlayerService
{
    public PlayerService(GarageClickerDbContext context) : base(context)
    {
    }

    public async Task<PlayerItem> GetPlayerItem(Guid playerId)
    {
        var playerItem = await _context.PlayerItems.FirstOrDefaultAsync(pi => pi.PlayerId.Equals(playerId));
        if (playerItem == null)
        {
            throw new Exception($"PlayerItem with playerId {playerId} not found");
        }
        return playerItem;
    }

    public Task<List<PlayerItem>> GetPlayerItems()
    {
        var items = _context.PlayerItems.ToListAsync();
        return items;
    }

    public Task<Player> GetPlayerById(Guid id)
    {
        var player = _context.Players.FirstOrDefaultAsync(p => p.Id.Equals(id));
        if (player == null)
        {
            throw new Exception($"Player with id {id} not found");
        }
        return player;
    }

    public async Task<Player> AddPlayer(CreateUserDTO player)
    {
        IPasswordHasher<Player> passwordHasher = new PasswordHasher<Player>();
        var newPlayer = player.toPlayer();
        newPlayer.PasswordHash = passwordHasher.HashPassword(newPlayer, player.Password);
        _context.Players.Add(newPlayer);
        await _context.SaveChangesAsync();
        return newPlayer;
    }

    public async Task<Player> LoginPlayer(LoginDetailsDTO loginDetails)
    {
        var player = await _context.Players.FirstOrDefaultAsync(p => p.Username.Equals(loginDetails.Username));
        if (player == null)
        {
            throw new Exception($"Player with username {loginDetails.Username} not found");
        }
        IPasswordHasher<Player> passwordHasher = new PasswordHasher<Player>();
        var result = passwordHasher.VerifyHashedPassword(player, player.PasswordHash, loginDetails.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new Exception("Invalid password");
        }
        return player;
    }

    public async Task<Player> SavePlayer(SavePlayerRequestDTO request)
    {
        var player = await _context.Players
            .FirstOrDefaultAsync(p => p.Id == request.PlayerId);

        if (player is null)
            throw new Exception($"Player with id {request.PlayerId} not found");

        var now = DateTime.UtcNow;
        var elapsedSeconds = (now - player.LastSaveAt).TotalSeconds;

        if (elapsedSeconds > 0)
        {
            var passiveIncome = player.ClicksPerSecond * (decimal)elapsedSeconds;
            player.Credits += (long)Math.Floor(passiveIncome);
        }

        player.Credits = Math.Max(player.Credits, request.ClientCredits);
        player.TotalClicks = request.ClientTotalClicks;

        player.LastSaveAt = now;

        await _context.SaveChangesAsync();
        return player;
    }

    public async Task AddAllItemsToPlayer(Guid playerId, IEnumerable<Item> items)
    {
        var playerItems = items.Select(item => new PlayerItem
        {
            Id = Guid.NewGuid(),
            PlayerId = playerId,
            ItemId = item.Id,
            Quantity = 0
        });

        await _context.PlayerItems.AddRangeAsync(playerItems);
        await _context.SaveChangesAsync();
        
    }
    
    public async Task<Player> GetPlayerWithItems(Guid playerId)
    {
        return await _context.Players
            .Include(p => p.PlayerItems)
            .ThenInclude(pi => pi.Item)
            .FirstAsync(p => p.Id == playerId);
    }
}