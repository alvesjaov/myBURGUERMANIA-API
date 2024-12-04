namespace myBURGUERMANIA_API.DTOs.Order
{
    public class CreateOrderDto
    {
        public required string UserId { get; set; }
        public required string StatusId { get; set; } // Adicionar StatusId
        public required string SelectedProductsId { get; set; } // Adicionar SelectedProductsId
    }
}