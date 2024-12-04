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

        public SelectedProductsDto Create(List<string> productIds)
        {
            try
            {
                var selectedProducts = new SelectedProducts
                {
                    Id = IdHelper.GenerateRandomId(),
                    ProductIds = productIds
                };
                _context.SelectedProducts.Add(selectedProducts);
                _context.SaveChanges();

                var products = _context.Products.Where(p => productIds.Contains(p.Id)).ToList();

                return new SelectedProductsDto
                {
                    Id = selectedProducts.Id,
                    ProductIds = selectedProducts.ProductIds,
                    ProductNames = products.Select(p => p.Title).ToList(),
                    ProductImageUrls = products.Select(p => p.Image).ToList()
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
                    ProductImageUrls = products.Select(p => p.Image).ToList()
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
    }
}