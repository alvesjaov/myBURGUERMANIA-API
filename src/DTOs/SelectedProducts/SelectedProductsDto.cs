
namespace myBURGUERMANIA_API.DTOs.SelectedProducts
{
    public class SelectedProductsDto
    {
        public required string Id { get; set; }
        public required List<string> ProductIds { get; set; } // Lista de IDs de produtos selecionados
    }
}