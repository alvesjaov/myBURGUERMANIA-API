namespace myBURGUERMANIA_API.DTOs.SelectedProducts
{
    public class CreateSelectedProductsDto
    {
        public required List<string> ProductIds { get; set; } // Lista de IDs de produtos selecionados
        public required string UserId { get; set; } // ID do usuário que criou a seleção de produtos
    }
}