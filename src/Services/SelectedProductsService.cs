using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.Data;
using myBURGUERMANIA_API.Helpers; // Add this line to import the IdHelper class
using myBURGUERMANIA_API.DTOs.SelectedProducts; // Adicionar importação para DTO

namespace myBURGUERMANIA_API.Services
{
    public class SelectedProductsService
    {
        private readonly ApplicationDbContext _context;

        public SelectedProductsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ProductExists(string productId)
        {
            return _context.Products.Any(p => p.Id == productId);
        }

        public SelectedProductsDto Create(List<string> productIds, string userId)
        {
            try
            {
                if (productIds == null || !productIds.Any())
                {
                    throw new ArgumentException("A lista de IDs de produtos não pode estar vazia.");
                }

                foreach (var productId in productIds)
                {
                    if (string.IsNullOrWhiteSpace(productId))
                    {
                        throw new ArgumentException("IDs de produtos não podem ser nulos ou vazios.");
                    }

                    if (!ProductExists(productId))
                    {
                        throw new KeyNotFoundException($"Produto com ID {productId} não encontrado.");
                    }
                }

                var selectedProducts = new SelectedProducts
                {
                    Id = IdHelper.GenerateRandomId(),
                    ProductIds = productIds,
                    UserId = userId
                };
                _context.SelectedProducts.Add(selectedProducts);
                _context.SaveChanges();

                var products = _context.Products.Where(p => productIds.Contains(p.Id)).ToList();

                return new SelectedProductsDto
                {
                    Id = selectedProducts.Id,
                    ProductIds = selectedProducts.ProductIds,
                    ProductNames = products.Select(p => p.Title).ToList(),
                    ProductImageUrls = products.Select(p => p.Image).ToList(),
                    UserId = selectedProducts.UserId
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ocorreu um erro ao criar os produtos selecionados.", ex);
            }
        }

        public SelectedProductsDto GetById(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new ArgumentException("O ID não pode ser nulo ou vazio.");
                }

                var selectedProducts = _context.SelectedProducts.Find(id);
                if (selectedProducts == null)
                {
                    throw new KeyNotFoundException("Produtos selecionados não encontrados.");
                }

                var products = _context.Products.Where(p => selectedProducts.ProductIds.Contains(p.Id)).ToList();

                return new SelectedProductsDto
                {
                    Id = selectedProducts.Id,
                    ProductIds = selectedProducts.ProductIds,
                    ProductNames = products.Select(p => p.Title).ToList(),
                    ProductImageUrls = products.Select(p => p.Image).ToList(),
                    UserId = selectedProducts.UserId
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ocorreu um erro ao recuperar os produtos selecionados.", ex);
            }
        }

        public void Remove(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new ArgumentException("O ID não pode ser nulo ou vazio.");
                }

                var selectedProducts = _context.SelectedProducts.Find(id);
                if (selectedProducts != null)
                {
                    _context.SelectedProducts.Remove(selectedProducts);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ocorreu um erro ao remover os produtos selecionados.", ex);
            }
        }

        public SelectedProductsDto AddProductIds(string id, List<string> productIds)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new ArgumentException("O ID não pode ser nulo ou vazio.");
                }

                if (productIds == null || !productIds.Any())
                {
                    throw new ArgumentException("A lista de IDs de produtos não pode estar vazia.");
                }

                var selectedProducts = _context.SelectedProducts.Find(id);
                if (selectedProducts == null)
                {
                    throw new KeyNotFoundException("Produtos selecionados não encontrados.");
                }

                foreach (var productId in productIds)
                {
                    if (!ProductExists(productId))
                    {
                        throw new KeyNotFoundException($"Produto com ID {productId} não encontrado.");
                    }
                }

                selectedProducts.ProductIds.AddRange(productIds);
                _context.SelectedProducts.Update(selectedProducts);
                _context.SaveChanges();

                var products = _context.Products.Where(p => selectedProducts.ProductIds.Contains(p.Id)).ToList();

                return new SelectedProductsDto
                {
                    Id = selectedProducts.Id,
                    ProductIds = selectedProducts.ProductIds,
                    ProductNames = products.Select(p => p.Title).ToList(),
                    ProductImageUrls = products.Select(p => p.Image).ToList(),
                    UserId = selectedProducts.UserId
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ocorreu um erro ao adicionar IDs de produtos.", ex);
            }
        }

        public SelectedProductsDto RemoveProductId(string selectedProductsId, string productId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(selectedProductsId))
                {
                    throw new ArgumentException("O ID de produtos selecionados não pode ser nulo ou vazio.");
                }

                if (string.IsNullOrWhiteSpace(productId))
                {
                    throw new ArgumentException("O ID do produto não pode ser nulo ou vazio.");
                }

                var selectedProducts = _context.SelectedProducts.Find(selectedProductsId);
                if (selectedProducts == null)
                {
                    throw new KeyNotFoundException("Produtos selecionados não encontrados.");
                }

                if (!selectedProducts.ProductIds.Contains(productId))
                {
                    throw new KeyNotFoundException($"Produto com ID {productId} não encontrado na seleção.");
                }

                selectedProducts.ProductIds.RemoveAll(id => id == productId);
                _context.SelectedProducts.Update(selectedProducts);
                _context.SaveChanges();

                var products = _context.Products.Where(p => selectedProducts.ProductIds.Contains(p.Id)).ToList();

                return new SelectedProductsDto
                {
                    Id = selectedProducts.Id,
                    ProductIds = selectedProducts.ProductIds,
                    ProductNames = products.Select(p => p.Title).ToList(),
                    ProductImageUrls = products.Select(p => p.Image).ToList(),
                    UserId = selectedProducts.UserId
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ocorreu um erro ao remover o ID do produto.", ex);
            }
        }
    }
}