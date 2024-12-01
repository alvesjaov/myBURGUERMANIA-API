using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myBURGUERMANIA_API.Models
{
    public enum ProductCategory
    {
        Hamburguer = 1,
        Porcao = 2,
        Bebida = 3
    }

    public class Product
    {
        [Key]
        [StringLength(36)]
        public required string Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Title { get; set; }

        [Required]
        public required double Price { get; set; }

        [Required]
        [StringLength(255)]
        public required string Description { get; set; }

        [Required]
        [StringLength(100)]
        public required string Image { get; set; }

        [Required]
        public required string CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public required Category Category { get; set; }
    }
}
