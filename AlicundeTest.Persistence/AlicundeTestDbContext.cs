using AlicundeTest.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AlicundeTest.Persistence;

public class AlicundeTestDbContext : DbContext
{
    public AlicundeTestDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Bank> Banks { get; set; }
}
