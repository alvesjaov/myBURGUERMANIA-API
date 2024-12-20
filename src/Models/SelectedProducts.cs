using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myBURGUERMANIA_API.Models
{
    public class SelectedProducts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required string Id { get; set; }
        [Required]
        public required List<string> ProductIds { get; set; } // Lista de IDs de produtos selecionados
        [Required]
        public required string UserId { get; set; } // ID do usuário que criou a seleção de produtos
    }
}