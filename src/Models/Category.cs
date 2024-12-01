using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace myBURGUERMANIA_API.Models
{
    public class Category
    {
        [Key]
        public required string Id { get; set; } // Adicionado required

        [Required]
        [StringLength(50)]
        public required string Name { get; set; } // Adicionado required
        public ICollection<Product> Products { get; set; } = new List<Product>(); // Adicionado para definir a relação com produtos
    }
}