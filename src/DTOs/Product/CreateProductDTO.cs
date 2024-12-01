using myBURGUERMANIA_API.Models;

namespace myBURGUERMANIA_API.DTOs.Product
{
    public class CreateProductDto
    {
        public required string Title { get; set; }
        public double Price { get; set; }
        public required string Description { get; set; }
        public required string Image { get; set; }
        public required ProductCategory Category { get; set; } // Atualizado para usar enum
    }
}
