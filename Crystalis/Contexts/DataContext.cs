using Crystalis.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Crystalis.Contexts;

public class DataContext : IdentityDbContext<ApplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CampaignUser> CampaignUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CampaignUser>().HasKey(cu => new { cu.UserId, cu.CampaignId });
        
        // Configure relationships
        modelBuilder.Entity<Campaign>()
            .HasMany(c => c.Locations)
            .WithOne(l => l.Campaign)
            .HasForeignKey(l => l.CampaignID);
        modelBuilder.Entity<Campaign>()
            .HasOne(c => c.Author)
            .WithMany(l => l.Campaigns)
            .HasForeignKey(l => l.AuthorId);
            
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Characters)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);
            
        modelBuilder.Entity<CampaignUser>()
            .HasOne(cu => cu.User)
            .WithMany(u => u.CampaignUsers)
            .HasForeignKey(cu => cu.UserId);
            
        modelBuilder.Entity<CampaignUser>()
            .HasOne(cu => cu.Campaign)
            .WithMany(c => c.CampaignUsers)
            .HasForeignKey(cu => cu.CampaignId);
        
        
    }
}