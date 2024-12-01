using myBURGUERMANIA_API.DTOs;
using myBURGUERMANIA_API.Helpers;
using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.Data;

namespace myBURGUERMANIA_API.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;

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

        public Order Create(CreateOrderDto dto)
        {
            var newOrder = new Order
            {
                Id = IdHelper.GenerateRandomId(), // Gerar ID automaticamente
                UserId = dto.UserId,
                ProductIds = dto.ProductIds,
                Status = GetStatusName(dto.Status), // Converter status para string
                TotalValue = dto.TotalValue
            };
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            return newOrder;
        }

        public Order GetById(string id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                throw new ArgumentNullException(nameof(id), "Pedido não encontrado");
            }
            return order;
        }

        public Order Update(string id, UpdateOrderDto dto)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                throw new ArgumentNullException(nameof(id), "Pedido não encontrado");
            }
            order.Status = dto.Status.HasValue ? GetStatusName(dto.Status.Value) : order.Status; // Converter status para string
            order.ProductIds = dto.ProductIds ?? order.ProductIds;
            order.TotalValue = dto.TotalValue != 0 ? dto.TotalValue : order.TotalValue;
            _context.SaveChanges();
            return order;
        }
    }
}