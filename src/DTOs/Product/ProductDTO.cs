using myBURGUERMANIA_API.Models;

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
        }

        public string Id { get; set; } // Alterado para string
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
