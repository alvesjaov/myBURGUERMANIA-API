namespace myBURGUERMANIA_API.DTOs.Order
{
    public class OrderDto
    {
        public required string Id { get; set; }
        public required string UserId { get; set; }
        public required List<string> ProductIds { get; set; }
        public required string Status { get; set; } 
        public decimal TotalValue { get; set; }
    }
}