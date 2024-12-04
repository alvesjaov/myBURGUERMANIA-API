namespace myBURGUERMANIA_API.DTOs.Order
{
    public class OrderDto
    {
        public required string Id { get; set; }
        public required string UserId { get; set; }
        public required List<string> ProductIds { get; set; }
        public required string StatusId { get; set; } // Adicionar StatusId
        public required string StatusName { get; set; } // Adicionar StatusName
        public List<string> ProductNames { get; set; } = new List<string>();
        public List<string> ProductImageUrls { get; set; } = new List<string>();
        public decimal TotalValue { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserCPF { get; set; } = string.Empty;
        public string UserPhoneNumber { get; set; } = string.Empty;
    }
}