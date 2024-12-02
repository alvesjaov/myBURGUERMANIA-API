namespace myBURGUERMANIA_API.DTOs
{
    public class CreateOrderDto
    {
        public required string UserId { get; set; }
        public required List<string> ProductIds { get; set; } // Alterar para List<string>
        public required string StatusId { get; set; } // Adicionar StatusId
    }
}