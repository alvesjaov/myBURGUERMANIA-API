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
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!; // Adicionar DbSet para Orders
        public DbSet<Category> Categories { get; set; } = null!; // Adicionar DbSet para Categories

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id); // Definindo Id como chave primária
                entity.Property(e => e.Id)
                    .HasMaxLength(36) // Definir comprimento máximo
                    .IsRequired()
                    .HasColumnType("varchar(36)"); // Configurando como string

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

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(255)"); 
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id); // Definindo Id como chave primária
                entity.Property(e => e.Id)
                    .HasMaxLength(36) // Definir comprimento máximo
                    .IsRequired()
                    .HasColumnType("varchar(36)"); // Configurando como string

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(36) // Atualizado para comprimento máximo de 36
                    .IsRequired()
                    .HasColumnType("varchar(36)"); // Nova coluna para CategoryId

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Products) // Atualizado para incluir a relação com produtos
                    .HasForeignKey(e => e.CategoryId); // Definir chave estrangeira para CategoryId
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id); // Definindo Id como chave primária
                entity.Property(e => e.Id)
                    .HasMaxLength(36) // Definir comprimento máximo
                    .IsRequired()
                    .HasColumnType("varchar(36)"); // Configurando como string

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .IsRequired()
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.TotalValue)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.ProductIds)
                    .IsRequired()
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    )
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id); // Definindo Id como chave primária
                entity.Property(e => e.Id)
                    .HasMaxLength(36) // Definir comprimento máximo
                    .IsRequired()
                    .HasColumnType("varchar(36)"); // Configurando como string

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasMany(c => c.Products) // Adicionado para incluir a relação com produtos
                    .WithOne(p => p.Category)
                    .HasForeignKey(p => p.CategoryId);
            });

            // Configurar relação entre User e Order
            modelBuilder.Entity<User>()
                .HasMany(u => u.OrderHistory)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);
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