using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.DTOs.Product;
using System.Collections.Generic;
using System.Linq;

namespace myBURGUERMANIA_API.DTOs.Category
{
    public class CategoryDto
    {
        public CategoryDto(Models.Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Products = category.Products.Select(p => new ProductInfoDto(p)).ToList();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public List<ProductInfoDto> Products { get; set; } // Adicionado para incluir produtos
    }

    public class ProductInfoDto
    {
        public ProductInfoDto(Models.Product product)
        {
            Id = product.Id;
            Title = product.Title;
            Price = product.Price;
            Description = product.Description;
            Image = product.Image;
            CategoryId = product.CategoryId;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string CategoryId { get; set; }
    }
}