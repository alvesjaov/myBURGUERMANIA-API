using myBURGUERMANIA_API.DTOs;
using myBURGUERMANIA_API.Helpers;
using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.Data;
using myBURGUERMANIA_API.DTOs.Order;

namespace myBURGUERMANIA_API.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;
        private const string UserNotFound = "Usuário não encontrado.";
        private const string ProductNotFound = "Pedido não encontrado.";

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        private static string GetStatusName(int status)
        {
            return status switch
            {
                1 => "Pendente",
                2 => "Em Preparação",
                3 => "Pronto para Entrega",
                4 => "Entregue",
                _ => throw new ArgumentException("Status inválido.")
            };
        }

        private decimal CalculateTotalValue(List<string> productIds)
        {
            var products = _context.Products.Where(p => productIds.Contains(p.Id)).ToList();
            decimal totalValue = 0;
            foreach (var productId in productIds)
            {
                var product = products.Find(p => p.Id == productId);
                if (product != null)
                {
                    totalValue += (decimal)product.Price;
                }
            }
            return totalValue;
        }

        public OrderDto Create(CreateOrderDto dto)
        {
            var user = _context.Users.Find(dto.UserId);
            if (user == null)
            {
                throw new InvalidOperationException(UserNotFound); // Lançar exceção se o usuário não for encontrado
            }

            var newOrder = new Order
            {
                Id = IdHelper.GenerateRandomId(), // Gerar ID automaticamente
                UserId = dto.UserId,
                ProductIds = dto.ProductIds,
                Status = GetStatusName(dto.Status), // Converter status para string
                TotalValue = CalculateTotalValue(dto.ProductIds) // Calcular valor total
            };
            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            return new OrderDto
            {
                Id = newOrder.Id,
                UserId = newOrder.UserId,
                ProductIds = newOrder.ProductIds,
                Status = newOrder.Status,
                TotalValue = newOrder.TotalValue,
                UserName = user.Name,
                UserCPF = user.CPF,
                UserPhoneNumber = user.PhoneNumber
            };
        }

        public OrderDto GetById(string id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                throw new KeyNotFoundException(ProductNotFound);
            }

            var user = _context.Users.Find(order.UserId);
            if (user == null)
            {
                throw new InvalidOperationException(UserNotFound);
            }

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                ProductIds = order.ProductIds,
                Status = order.Status,
                TotalValue = order.TotalValue,
                UserName = user.Name,
                UserCPF = user.CPF,
                UserPhoneNumber = user.PhoneNumber
            };
        }

        public OrderDto UpdateStatus(string id, int status)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                throw new KeyNotFoundException(ProductNotFound);
            }

            order.Status = GetStatusName(status); // Converter status para string
            _context.SaveChanges();

            var user = _context.Users.Find(order.UserId);
            if (user == null)
            {
                throw new InvalidOperationException(UserNotFound);
            }

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                ProductIds = order.ProductIds,
                Status = order.Status,
                TotalValue = order.TotalValue,
                UserName = user.Name,
                UserCPF = user.CPF,
                UserPhoneNumber = user.PhoneNumber
            };
        }

        public OrderDto Cancel(string id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                throw new KeyNotFoundException(ProductNotFound);
            }

            order.Status = "Cancelado"; // Marcar o pedido como cancelado
            _context.SaveChanges();

            var user = _context.Users.Find(order.UserId);
            if (user == null)
            {
                throw new InvalidOperationException(UserNotFound);
            }

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                ProductIds = order.ProductIds,
                Status = order.Status,
                TotalValue = order.TotalValue,
                UserName = user.Name,
                UserCPF = user.CPF,
                UserPhoneNumber = user.PhoneNumber
            };
        }

    }
}