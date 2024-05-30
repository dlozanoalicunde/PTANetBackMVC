// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using MBAOptionsManager.Model;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<MBAOption> MBAOptions { get; set; }
    public DbSet<MBA> MBAs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MBAOption>()
            .HasMany(m => m.MBAs)
            .WithOne(m => m.MBAOption)
            .HasForeignKey(m => m.MBAOptionId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}