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
        private readonly StatusService _statusService;
        private const string UserNotFound = "Usuário não encontrado.";
        private const string ProductNotFound = "Pedido não encontrado.";

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
            _statusService = new StatusService(context);
        }

        public static decimal CalculateTotalValue(ApplicationDbContext context, List<string> productIds)
        {
            var products = context.Products.Where(p => productIds.Contains(p.Id)).ToList();
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

            var selectedProducts = _context.SelectedProducts.Find(dto.SelectedProductsId);
            if (selectedProducts == null)
            {
                throw new InvalidOperationException("SelectedProducts não encontrado."); // Lançar exceção se SelectedProducts não for encontrado
            }

            var status = _statusService.GetStatusById(dto.StatusId); // Obter status pelo ID
            var newOrder = new Order
            {
                Id = IdHelper.GenerateRandomId(), // Usar o helper para gerar ID aleatório
                UserId = dto.UserId,
                SelectedProductsId = dto.SelectedProductsId, // Atribuir SelectedProductsId
                StatusId = status.Id, // Atribuir ID do status
                TotalValue = CalculateTotalValue(_context, selectedProducts.ProductIds) // Calcular valor total
            };
            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            var products = _context.Products.Where(p => selectedProducts.ProductIds.Contains(p.Id)).ToList();

            // Adicionar pedido ao histórico do usuário
            var userService = new UserService(_context);
            userService.AddOrderToUserHistory(dto.UserId, newOrder);

            return new OrderDto
            {
                Id = newOrder.Id,
                UserId = newOrder.UserId,
                SelectedProductsId = newOrder.SelectedProductsId, // Atribuir SelectedProductsId
                ProductIds = selectedProducts.ProductIds,
                ProductNames = products.Select(p => p.Title).ToList(),
                ProductImageUrls = products.Select(p => p.Image).ToList(),
                StatusId = newOrder.StatusId,
                StatusName = status.Name, // Atribuir nome do status
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
                throw new KeyNotFoundException(ProductNotFound); // Lançar exceção se o pedido não for encontrado
            }

            var user = _context.Users.Find(order.UserId);
            if (user == null)
            {
                throw new InvalidOperationException(UserNotFound);
            }

            var selectedProducts = _context.SelectedProducts.Find(order.SelectedProductsId);
            var products = _context.Products.Where(p => selectedProducts.ProductIds.Contains(p.Id)).ToList();

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                SelectedProductsId = order.SelectedProductsId, // Atribuir SelectedProductsId
                ProductIds = selectedProducts.ProductIds,
                ProductNames = products.Select(p => p.Title).ToList(),
                ProductImageUrls = products.Select(p => p.Image).ToList(),
                StatusId = order.StatusId,
                StatusName = _statusService.GetStatusById(order.StatusId).Name, // Atribuir nome do status
                TotalValue = order.TotalValue,
                UserName = user.Name,
                UserCPF = user.CPF,
                UserPhoneNumber = user.PhoneNumber
            };
        }

        public OrderDto UpdateStatus(string id, string statusId)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                throw new KeyNotFoundException(ProductNotFound); // Lançar exceção se o pedido não for encontrado
            }

            var status = _statusService.GetStatusById(statusId); // Obter status pelo ID
            order.StatusId = status.Id; // Atualizar ID do status
            _context.SaveChanges();

            var user = _context.Users.Find(order.UserId);
            if (user == null)
            {
                throw new InvalidOperationException(UserNotFound);
            }

            var selectedProducts = _context.SelectedProducts.Find(order.SelectedProductsId);
            var products = _context.Products.Where(p => selectedProducts.ProductIds.Contains(p.Id)).ToList();

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                SelectedProductsId = order.SelectedProductsId, // Atribuir SelectedProductsId
                ProductIds = selectedProducts.ProductIds,
                ProductNames = products.Select(p => p.Title).ToList(),
                ProductImageUrls = products.Select(p => p.Image).ToList(),
                StatusId = order.StatusId,
                StatusName = status.Name, // Atribuir nome do status
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
                throw new KeyNotFoundException(ProductNotFound); // Lançar exceção se o pedido não for encontrado
            }

            var status = _context.Statuses.FirstOrDefault(s => s.Name == "Cancelado");
            if (status == null)
            {
                throw new ArgumentException("Status 'Cancelado' não encontrado.");
            }

            order.StatusId = status.Id; // Atualizar ID do status para 'Cancelado'
            _context.SaveChanges();

            var user = _context.Users.Find(order.UserId);
            if (user == null)
            {
                throw new InvalidOperationException(UserNotFound);
            }

            var selectedProducts = _context.SelectedProducts.Find(order.SelectedProductsId);
            var products = _context.Products.Where(p => selectedProducts.ProductIds.Contains(p.Id)).ToList();

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                SelectedProductsId = order.SelectedProductsId, // Atribuir SelectedProductsId
                ProductIds = selectedProducts.ProductIds,
                ProductNames = products.Select(p => p.Title).ToList(),
                ProductImageUrls = products.Select(p => p.Image).ToList(),
                StatusId = order.StatusId,
                StatusName = status.Name, // Atribuir nome do status
                TotalValue = order.TotalValue,
                UserName = user.Name,
                UserCPF = user.CPF,
                UserPhoneNumber = user.PhoneNumber
            };
        }

    }
}