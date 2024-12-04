namespace myBURGUERMANIA_API.DTOs.SelectedProducts
{
    public class SelectedProductsDto
    {
        public required string Id { get; set; }
        public required List<string> ProductIds { get; set; }
        public List<string> ProductNames { get; set; } = new List<string>();
        public List<string> ProductImageUrls { get; set; } = new List<string>();
    }
}