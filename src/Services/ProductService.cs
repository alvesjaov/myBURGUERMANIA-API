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
                throw new KeyNotFoundException("Produto não encontrado.");
            }
            return new ProductDto(product);
        }

        private static string GetCategoryName(int category)
        {
            return category switch
            {
                1 => "Hambúrguer",
                2 => "Porção",
                3 => "Bebida",
                4 => "Sobremesa",
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
                throw new KeyNotFoundException("Produto não encontrado.");
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
                throw new KeyNotFoundException("Produto não encontrado.");
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
