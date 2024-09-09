using Microsoft.EntityFrameworkCore;
using PTANetBackMVC.Models;

namespace PTANetBackMVC
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Agregamos el DbSet para BalanceResponsibleParty
        public DbSet<BalanceResponsibleParty> BalanceResponsibleParties { get; set; }
    }
}
