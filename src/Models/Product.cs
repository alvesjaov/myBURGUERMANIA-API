using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myBURGUERMANIA_API.Models
{
    public class Product
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; } // Alterado para string

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Image { get; set; }
    }
}
