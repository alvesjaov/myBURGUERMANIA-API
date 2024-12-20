using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public required string StatusId { get; set; } // Adicionar chave estrangeira para Status
        [ForeignKey("StatusId")]
        public Status Status { get; set; } // Adicionar navegação para Status
        [Required]
        public required string SelectedProductsId { get; set; } // Adicionar referência para SelectedProducts
        [ForeignKey("SelectedProductsId")]
        public SelectedProducts SelectedProducts { get; set; } // Adicionar navegação para SelectedProducts
        [Required]
        public decimal TotalValue { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore] // Ignorar a propriedade User na serialização JSON
        public User User { get; set; }
    }
}

