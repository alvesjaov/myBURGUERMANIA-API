using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.Data;
using myBURGUERMANIA_API.Helpers; // Add this line to import the IdHelper class

namespace myBURGUERMANIA_API.Services
{
    public class SelectedProductsService
    {
        private readonly ApplicationDbContext _context;

        public SelectedProductsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public SelectedProducts Create(List<string> productIds)
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
                return selectedProducts;
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("Ocorreu um erro ao criar os produtos selecionados.", ex);
            }
        }

        public SelectedProducts GetById(string id)
        {
            try
            {
                var selectedProducts = _context.SelectedProducts.Find(id);
                if (selectedProducts == null)
                {
                    throw new KeyNotFoundException("Produtos selecionados não encontrados.");
                }
                return selectedProducts;
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