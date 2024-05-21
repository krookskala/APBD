using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Models;

namespace VeterinaryClinic.Services
{
    public class AnimalContext : DbContext
    {
        
        public AnimalContext()
        {
            
        }
        
        public AnimalContext(DbContextOptions<AnimalContext> options) : base(options)
        {
            
        }
        
        public DbSet<Animal> Animals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Animal");
                entity.Property(e => e.Name).HasMaxLength(200);
                entity.Property(e => e.Category).HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(200);
                entity.Property(e => e.Area).HasMaxLength(200);
            });
        }
    }
}