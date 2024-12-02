using System.Collections.Generic;
using System.Linq;
using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.DTOs.Product;
using myBURGUERMANIA_API.DTOs.Category;
using myBURGUERMANIA_API.Data;
using myBURGUERMANIA_API.Helpers;
using Microsoft.EntityFrameworkCore;

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
            return _context.Products.Include(p => p.Category).Select(p => new ProductDto(p)
            {
                CategoryName = p.Category.Name // Adicionar o nome da categoria
            });
        }

        public ProductDto? GetById(string id)
        {
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new KeyNotFoundException(ProductNotFound);
            }
            return new ProductDto(product)
            {
                CategoryName = product.Category.Name // Adicionar o nome da categoria
            };
        }

        public IEnumerable<ProductDto> GetByCategory(string categoryId)
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId) // Comparar o ID da categoria
                .Select(p => new ProductDto(p)
                {
                    CategoryName = p.Category.Name // Adicionar o nome da categoria
                });
        }

        public ProductDto Create(CreateProductDto dto)
        {
            if (_context.Products.Any(p => p.Title == dto.Title))
            {
                throw new ArgumentException("Já existe um produto com este nome.");
            }
            var category = _context.Categories.Find(dto.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }
            var newProduct = new Product
            {
                Id = IdHelper.GenerateRandomId(), // Usar o helper para gerar ID aleatório
                Title = dto.Title,
                Price = dto.Price,
                Description = dto.Description,
                Image = dto.Image,
                CategoryId = dto.CategoryId, // Armazenar apenas o ID da categoria
                Category = category // Inicializar a propriedade Category
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return new ProductDto(newProduct)
            {
                CategoryName = newProduct.Category.Name // Adicionar o nome da categoria
            };
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
            product.CategoryId = dto.CategoryId; // Armazenar apenas o ID da categoria
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