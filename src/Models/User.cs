using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myBURGUERMANIA_API.Models
{
    public class User
    {
        [Key]
        [Required]
        [StringLength(36)]
        public string Id { get; set; } = string.Empty; // Definir comprimento máximo
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(30)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [RegularExpression(@"\d{11}", ErrorMessage = "CPF deve ter 11 dígitos")]
        public string CPF { get; set; } = string.Empty;
        
        [Required]
        public DateTime BirthDate { get; set; }
        
        [Required]
        [Phone]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty; // Novo campo de senha
    }
}
