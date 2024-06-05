using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Models;

namespace VeterinaryClinic.Services
{
    public class AnimalContext : DbContext
    {
        public AnimalContext()
        {
            
        }
        
        public AnimalContext(DbContextOptions<AnimalContext> options) : base(options) { }

        public virtual DbSet<Animal> Animals { get; set; } = null!;
        public virtual DbSet<AnimalTypes> AnimalTypes { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Visit> Visits { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Animal");
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(2000);
                entity.Property(e => e.RowVersion).IsRowVersion();

                entity.HasOne(a => a.AnimalType)
                    .WithMany(at => at.Animals)
                    .HasForeignKey(a => a.AnimalTypesId)
                    .IsRequired();
            });

            modelBuilder.Entity<AnimalTypes>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("AnimalTypes");
                entity.Property(e => e.Name).HasMaxLength(150).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Employee");
                entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
                entity.Property(e => e.PhoneNumber).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(200).IsRequired();
                entity.Property(e => e.RowVersion).IsRowVersion();

                entity.HasIndex(e => new { e.PhoneNumber, e.Email }).IsUnique();
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Visit");
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.RowVersion).IsRowVersion();

                entity.HasOne(v => v.Employee)
                      .WithMany()
                      .HasForeignKey(v => v.EmployeeId);

                entity.HasOne(v => v.Animal)
                      .WithMany()
                      .HasForeignKey(v => v.AnimalId);
            });
        }
    }
}