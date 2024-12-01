using System.Collections.Generic;
using System.Linq;
using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.DTOs.Product;
using myBURGUERMANIA_API.Data;
using myBURGUERMANIA_API.Helpers;

namespace myBURGUERMANIA_API.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;
        private const string ProductNotFound = "Produto não encontrado.";

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductDto> GetAll() 
        {
            return _context.Products.Select(p => new ProductDto(p));
        }

        public ProductDto? GetById(string id) 
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                throw new KeyNotFoundException(ProductNotFound);
            }
            return new ProductDto(product);
        }

        public IEnumerable<ProductDto> GetByCategory(string category)
        {
            return _context.Products
                .Where(p => p.Category == category)
                .Select(p => new ProductDto(p));
        }

        private static string GetCategoryName(int category)
        {
            return category switch
            {
                1 => "Hambúrgueres",
                2 => "Porções",
                3 => "Bebidas",
                4 => "Sobremesas",
                _ => throw new ArgumentException("Categoria inválida.")
            };
        }

        public Product Create(CreateProductDto dto)
        {
            if (_context.Products.Any(p => p.Title == dto.Title))
            {
                throw new ArgumentException("Já existe um produto com este nome.");
            }
            var newProduct = new Product
            {
                Id = IdHelper.GenerateRandomId(), // Usar o helper para gerar ID aleatório
                Title = dto.Title,
                Price = dto.Price,
                Description = dto.Description,
                Image = dto.Image,
                Category = GetCategoryName((int)dto.Category) // Converter categoria para string
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return newProduct; // Retornar o produto criado
        }

        public void Update(string id, UpdateProductDto dto) 
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                throw new KeyNotFoundException(ProductNotFound);
            }
            product.Title = dto.Title;
            product.Price = dto.Price;
            product.Description = dto.Description;
            product.Image = dto.Image;
            product.Category = GetCategoryName((int)dto.Category); // Converter categoria para string
            _context.SaveChanges();
        }

        public void Delete(string id) 
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                throw new KeyNotFoundException(ProductNotFound);
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
