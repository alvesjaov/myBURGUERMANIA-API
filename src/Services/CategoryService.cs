using System.Collections.Generic;
using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.DTOs.Category;
using myBURGUERMANIA_API.Data;
using myBURGUERMANIA_API.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace myBURGUERMANIA_API.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Category Create(CreateCategoryDto dto)
        {
            if (_context.Categories.Any(c => c.Name == dto.Name))
            {
                throw new ArgumentException("Já existe uma categoria com este nome.");
            }

            var newCategory = new Category
            {
                Id = IdHelper.GenerateRandomId(), // Use IdHelper para gerar ID único
                Name = dto.Name
            };
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return newCategory;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _context.Categories.Select(c => new CategoryDto(c));
        }

        public CategoryDto? GetById(string id)
        {
            var category = _context.Categories
                .Include(c => c.Products) // Incluir os produtos associados
                .SingleOrDefault(c => c.Id == id);
            if (category == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }
            return new CategoryDto(category);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesWithProductsAsync()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Products = c.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Price = p.Price,
                        Description = p.Description,
                        Image = p.Image,
                        CategoryId = p.CategoryId,
                        Category = new Category { Id = c.Id, Name = c.Name } // Inicializar a propriedade Category
                    }).ToList()
                }).ToListAsync();
        }

        public void Update(string id, UpdateCategoryDto dto)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }
            category.Name = dto.Name;
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}