using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using myBURGUERMANIA_API.Models;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using MySql.EntityFrameworkCore.Extensions; // Adicione esta linha

namespace myBURGUERMANIA_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.CPF)
                    .HasMaxLength(11)
                    .IsRequired()
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.BirthDate).HasColumnType("date").IsRequired();

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsRequired()
                    .HasColumnType("varchar(15)");
            });
        }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is not found.");
            }

            optionsBuilder.UseMySQL(connectionString); // UseMySQL ao invés de UseMySql

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}