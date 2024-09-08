using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Fee> Fees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Fee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Country)
                    .IsRequired();

                entity.Property(e => e.HourlyImbalanceFee)
                    .HasColumnType("decimal(10,4)");

                entity.Property(e => e.ImbalanceFee)
                    .HasColumnType("decimal(10,4)");

                entity.Property(e => e.PeakLoadFee)
                    .HasColumnType("decimal(10,4)"); ;

                entity.Property(e => e.Timestamp)
                    .IsRequired();

                entity.Property(e => e.TimestampUTC)
                    .IsRequired();

                entity.Property(e => e.VolumeFee)
                    .HasColumnType("decimal(10,4)");

                entity.Property(e => e.WeeklyFee)
                    .HasColumnType("decimal(10,4)");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
