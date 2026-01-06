using GarageClickerBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageClickerBackend.Data;

public class GarageClickerDbContext : DbContext
{
    public GarageClickerDbContext(DbContextOptions<GarageClickerDbContext> options) : base(options) { }
    
    public DbSet<Player> Players { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<PlayerItem> PlayerItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PlayerItem>()
            .HasKey(pi => pi.Id);

        modelBuilder.Entity<PlayerItem>()
            .HasOne(pi => pi.Player)
            .WithMany(p => p.PlayerItems)
            .HasForeignKey(pi => pi.PlayerId);

        modelBuilder.Entity<PlayerItem>()
            .HasOne(pi => pi.Item)
            .WithMany()
            .HasForeignKey(pi => pi.ItemId);
        
        modelBuilder.Entity<PlayerItem>()
            .HasIndex(x => new { x.PlayerId, x.ItemId })
            .IsUnique();
        
    }
}