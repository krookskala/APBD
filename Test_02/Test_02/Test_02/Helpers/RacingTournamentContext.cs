using Microsoft.EntityFrameworkCore;
using Test_02.Models;

public class RacingTournamentContext : DbContext
{
    public RacingTournamentContext(DbContextOptions<RacingTournamentContext> options)
        : base(options)
    {
    }

    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Competition> Competitions { get; set; }
    public DbSet<DriverCompetition> DriverCompetitions { get; set; }
    public DbSet<CarManufacturer> CarManufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DriverCompetition>()
            .HasKey(dc => new { dc.DriverId, dc.CompetitionId });

        modelBuilder.Entity<DriverCompetition>()
            .HasOne(dc => dc.Driver)
            .WithMany(d => d.DriverCompetitions)
            .HasForeignKey(dc => dc.DriverId);

        modelBuilder.Entity<DriverCompetition>()
            .HasOne(dc => dc.Competition)
            .WithMany(c => c.DriverCompetitions)
            .HasForeignKey(dc => dc.CompetitionId);
    }
}