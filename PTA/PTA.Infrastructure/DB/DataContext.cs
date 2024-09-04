using Microsoft.EntityFrameworkCore;
using PTA.Models;

namespace PTA.Infrastructure.DB
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<DistributionSystemOperator> DistributionSystemOperator { get; set; }
    }
}