namespace myBURGUERMANIA_API.Models
{
    public class Product
    {
        public string Id { get; set; } // Alterado para string
        public required string Title { get; set; }
        public double Price { get; set; }
        public required string Description { get; set; }
        public required string Image { get; set; }
    }
}
