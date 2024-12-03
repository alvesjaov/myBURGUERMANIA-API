using myBURGUERMANIA_API.Models;

namespace myBURGUERMANIA_API.DTOs.Product
{
    public class UpdateProductDto
    {
        public required string Title { get; set; }
        public double Price { get; set; }
        public required string Description { get; set; }
        public required string Image { get; set; }
        public required string CategoryId { get; set; } // Adicionado para armazenar o ID da categoria
    }
}
