using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myBURGUERMANIA_API.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required string Id { get; set; }
        [Required]
        public required string UserId { get; set; }
        [Required]
        public required List<string> ProductIds { get; set; } // Alterar para List<string>
        [Required]
        public required string Status { get; set; } = string.Empty; // Inicializar com valor padrão
        [Required]
        public decimal TotalValue { get; set; }
    }

}

