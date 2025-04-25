using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProphetCrystal.Enums;
using ProphetCrystal.Models;

namespace ProphetCrystal.Contexts;

public class DataContext : IdentityDbContext<ApplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<World> Worlds { get; set; }
    public DbSet<Location> Locations { set; get; }
    public DbSet<Person> People { set; get; }
    public DbSet<Organization> Organizations { set; get; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CampaignUser> CampaignUsers { get; set; }
    public DbSet<CampaignQueue> CampaignQueues { get; set; }

    public DbSet<Note> Notes { get; set; }
    public DbSet<WorldNote> WorldNotes { get; set; }
    public DbSet<LocationNote> LocationNotes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Relations

        builder.Entity<ApplicationUser>()
            .HasMany(u => u.Characters)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

        builder.Entity<Note>()
            .HasDiscriminator<NoteType>("OwnerType")
            .HasValue<WorldNote>(NoteType.WorldNote)
            .HasValue<LocationNote>(NoteType.LocationNote);


        builder.Entity<World>()
            .HasMany(c => c.Campaigns)
            .WithOne(l => l.World)
            .HasForeignKey(l => l.WorldId);
        builder.Entity<World>()
            .HasMany(c => c.Locations)
            .WithOne(l => l.World)
            .HasForeignKey(l => l.WorldId);
        builder.Entity<World>()
            .HasMany(c => c.People)
            .WithOne(l => l.World)
            .HasForeignKey(l => l.WorldId);
        builder.Entity<World>()
            .HasMany(c => c.Organizations)
            .WithOne(l => l.World)
            .HasForeignKey(l => l.WorldId);
        builder.Entity<World>()
            .HasOne(c => c.Author)
            .WithMany(l => l.Worlds)
            .HasForeignKey(l => l.AuthorId);

        builder.Entity<Location>()
            .HasMany(c => c.ChildrenLocations)
            .WithOne(l => l.ParentLocation)
            .HasForeignKey(l => l.ParentLocationId);
        builder.Entity<Location>()
            .HasOne(c => c.Author)
            .WithMany(l => l.Locations)
            .HasForeignKey(l => l.AuthorId);
        builder.Entity<Location>()
            .HasOne(c => c.World)
            .WithMany(l => l.Locations)
            .HasForeignKey(l => l.WorldId);

        builder.Entity<Organization>()
            .HasOne(c => c.World)
            .WithMany(l => l.Organizations)
            .HasForeignKey(l => l.WorldId);
        builder.Entity<Organization>()
            .HasOne(c => c.Location)
            .WithMany(l => l.Organizations)
            .HasForeignKey(l => l.LocationId);
        builder.Entity<Organization>()
            .HasOne(c => c.Author)
            .WithMany(l => l.Organizations)
            .HasForeignKey(l => l.AuthorId);

        builder.Entity<Person>()
            .HasOne(c => c.World)
            .WithMany(l => l.People)
            .HasForeignKey(l => l.WorldId);
        builder.Entity<Person>()
            .HasOne(c => c.Location)
            .WithMany(l => l.People)
            .HasForeignKey(l => l.LocationId);
        builder.Entity<Person>()
            .HasOne(c => c.Author)
            .WithMany(l => l.People)
            .HasForeignKey(l => l.AuthorId);

        builder.Entity<Campaign>()
            .HasOne(c => c.Author)
            .WithMany(l => l.Campaigns)
            .HasForeignKey(l => l.AuthorId);

        builder.Entity<CampaignUser>().HasKey(cu => new { cu.UserId, cu.CampaignId });
        builder.Entity<CampaignQueue>().HasKey(cu => new { cu.UserId, cu.CampaignId });

        builder.Entity<CampaignUser>()
            .HasOne(cu => cu.User)
            .WithMany(u => u.CampaignUsers)
            .HasForeignKey(cu => cu.UserId);

        builder.Entity<CampaignUser>()
            .HasOne(cu => cu.Campaign)
            .WithMany(c => c.CampaignUsers)
            .HasForeignKey(cu => cu.CampaignId);

        builder.Entity<CampaignQueue>()
            .HasOne(cu => cu.User)
            .WithMany(u => u.CampaignQueues)
            .HasForeignKey(cu => cu.UserId);

        builder.Entity<CampaignQueue>()
            .HasOne(cu => cu.Campaign)
            .WithMany(u => u.CampaignQueues)
            .HasForeignKey(cu => cu.CampaignId);

        #endregion
    }
}