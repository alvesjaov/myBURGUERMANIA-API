using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.Helpers;
using Microsoft.EntityFrameworkCore;
using myBURGUERMANIA_API.Services;

namespace myBURGUERMANIA_API.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                SeedDatabase(context);
            }
        }

        public static void SeedDatabase(ApplicationDbContext context)
        {
            // Verificar se já existem usuários no banco de dados
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            // Adicionar categorias
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Id = IdHelper.GenerateRandomId(), Name = "Hambúrgueres" },
                    new Category { Id = IdHelper.GenerateRandomId(), Name = "Bebidas" },
                    new Category { Id = IdHelper.GenerateRandomId(), Name = "Porções" },
                    new Category { Id = IdHelper.GenerateRandomId(), Name = "Sobremesas" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // Adicionar status
            if (!context.Statuses.Any())
            {
                var statuses = new List<Status>
                {
                    new Status { Id = IdHelper.GenerateRandomId(), Name = "Pendente" },
                    new Status { Id = IdHelper.GenerateRandomId(), Name = "Em Preparação" },
                    new Status { Id = IdHelper.GenerateRandomId(), Name = "Pronto para Entrega" },
                    new Status { Id = IdHelper.GenerateRandomId(), Name = "Entregue" },
                    new Status { Id = IdHelper.GenerateRandomId(), Name = "Cancelado" }
                };
                context.Statuses.AddRange(statuses);
                context.SaveChanges();
            }

            // Adicionar usuários
            context.Users.AddRange(
                new User
                {
                    Id = IdHelper.GenerateRandomId(),
                    Name = "Admin User",
                    Email = "admin@myburguer.com",
                    CPF = "12014504032",
                    BirthDate = new DateTime(1990, 1, 1),
                    PhoneNumber = "77998253614",
                    Password = PasswordHelper.HashPassword("Admin@123"),
                    OrderHistory = new List<Order>()
                },
                new User
                {
                    Id = IdHelper.GenerateRandomId(),
                    Name = "Test User",
                    Email = "test@myburguer.com",
                    CPF = "05973875083",
                    BirthDate = new DateTime(1995, 5, 5),
                    PhoneNumber = "77999121514",
                    Password = PasswordHelper.HashPassword("Test@123"),
                    OrderHistory = new List<Order>()
                }
            );
            context.SaveChanges();

            // Adicionar produtos
            var categoryHamburgueres = context.Categories.FirstOrDefault(c => c.Name == "Hambúrgueres");
            var categoryBebidas = context.Categories.FirstOrDefault(c => c.Name == "Bebidas");
            var categoryPorcoes = context.Categories.FirstOrDefault(c => c.Name == "Porções");

            if (categoryHamburgueres != null && categoryBebidas != null && categoryPorcoes != null && !context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Id = IdHelper.GenerateRandomId(),
                        Title = "Hambúrguer Vegano",
                        Description = "Pão, carne de soja, alface, tomate, maionese vegana",
                        Image = "https://shre.ink/hamburguer-vegano-png",
                        CategoryId = categoryHamburgueres.Id,
                        Category = categoryHamburgueres,
                        Price = 24.99
                    },
                    new Product
                    {
                        Id = IdHelper.GenerateRandomId(),
                        Title = "Hambúrguer de Frango",
                        Description = "Pão, frango, queijo, alface, tomate, maionese",
                        Image = "https://shre.ink/hamburguer-frango-png",
                        CategoryId = categoryHamburgueres.Id,
                        Category = categoryHamburgueres,
                        Price = 21.99
                    },
                    new Product
                    {
                        Id = IdHelper.GenerateRandomId(),
                        Title = "Hambúrguer de Bacon",
                        Description = "Pão, carne, bacon, queijo, alface, tomate, maionese",
                        Image = "https://shre.ink/hamburgue-bacon-png",
                        CategoryId = categoryHamburgueres.Id,
                        Category = categoryHamburgueres,
                        Price = 22.99
                    },
                    new Product
                    {
                        Id = IdHelper.GenerateRandomId(),
                        Title = "Hambúrguer Tradicional",
                        Description = "Pão, carne, queijo, alface, tomate, maionese",
                        Image = "https://shre.ink/hamburguer-tradicional-png",
                        CategoryId = categoryHamburgueres.Id,
                        Category = categoryHamburgueres,
                        Price = 21.58
                    },
                    new Product
                    {
                        Id = IdHelper.GenerateRandomId(),
                        Title = "Coca-cola Zero Lata",
                        Description = "Refrigerante de cola zero açúcar",
                        Image = "https://shre.ink/coca-cola-zero-jpeg",
                        CategoryId = categoryBebidas.Id,
                        Category = categoryBebidas,
                        Price = 5.99
                    },
                    new Product
                    {
                        Id = IdHelper.GenerateRandomId(),
                        Title = "Guaraná Lata",
                        Description = "Refrigerante de guaraná",
                        Image = "https://shre.ink/guarana-jpeg",
                        CategoryId = categoryBebidas.Id,
                        Category = categoryBebidas,
                        Price = 5.99
                    },
                    new Product
                    {
                        Id = IdHelper.GenerateRandomId(),
                        Title = "Coca-cola Lata",
                        Description = "Refrigerante de cola",
                        Image = "https://shre.ink/coca-cola-jpeg",
                        CategoryId = categoryBebidas.Id,
                        Category = categoryBebidas,
                        Price = 5.99
                    },
                    new Product
                    {
                        Id = IdHelper.GenerateRandomId(),
                        Title = "Porção de Nuggets",
                        Description = "Nuggets de frango crocantes",
                        Image = "https://shre.ink/porcao-nuggets-png",
                        CategoryId = categoryPorcoes.Id,
                        Category = categoryPorcoes,
                        Price = 21.99
                    },
                    new Product
                    {
                        Id = IdHelper.GenerateRandomId(),
                        Title = "Porção de Batata Frita",
                        Description = "Batata frita crocante",
                        Image = "https://shre.ink/porcao-batata-png",
                        CategoryId = categoryPorcoes.Id,
                        Category = categoryPorcoes,
                        Price = 13.99
                    },
                    new Product
                    {
                        Id = IdHelper.GenerateRandomId(),
                        Title = "Porção de Onion Rings",
                        Description = "Anéis de cebola empanados e fritos",
                        Image = "https://shre.ink/porcao-onion-png",
                        CategoryId = categoryPorcoes.Id,
                        Category = categoryPorcoes,
                        Price = 19.99
                    }
                );
                context.SaveChanges();
            }

            // Adicionar pedidos
            var statusPendente = context.Statuses.FirstOrDefault(s => s.Name == "Pendente");
            var userAdmin = context.Users.FirstOrDefault(u => u.Email == "admin@myburguer.com");
            if (statusPendente != null && userAdmin != null && !context.Orders.Any())
            {
                var product1 = context.Products.FirstOrDefault(p => p.Title == "Hambúrguer Vegano");
                var product2 = context.Products.FirstOrDefault(p => p.Title == "Hambúrguer de Frango");

                if (product1 != null && product2 != null)
                {
                    var productIds = new List<string> { product1.Id, product2.Id };
                    var totalValue = OrderService.CalculateTotalValue(context, productIds); // Usar o serviço para calcular o valor total

                    context.Orders.AddRange(
                        new Order
                        {
                            Id = IdHelper.GenerateRandomId(),
                            UserId = userAdmin.Id,
                            StatusId = statusPendente.Id,
                            ProductIds = productIds,
                            TotalValue = totalValue
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}