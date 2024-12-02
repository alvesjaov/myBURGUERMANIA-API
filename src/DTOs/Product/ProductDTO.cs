using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.DTOs.Category;

namespace myBURGUERMANIA_API.DTOs.Product
{
    public class ProductDto
    {
        public ProductDto(Models.Product product)
        {
            Id = product.Id;
            Title = product.Title;
            Price = product.Price;
            Description = product.Description;
            Image = product.Image;
            CategoryId = product.CategoryId; // Armazenar apenas o ID da categoria
            CategoryName = product.Category.Name; // Adicionar o nome da categoria
        }

        public string Id { get; set; } // Alterado para string
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string CategoryId { get; set; } // Armazenar apenas o ID da categoria
        public string CategoryName { get; set; } // Adicionar a propriedade CategoryName
    }
}
