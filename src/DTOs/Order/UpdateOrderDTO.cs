namespace myBURGUERMANIA_API.DTOs
{
    public class UpdateOrderDto
    {
        public int? Status { get; set; } // Alterar para int?
        public required List<string> ProductIds { get; set; }
        public required decimal TotalValue { get; set; }
    }
}
