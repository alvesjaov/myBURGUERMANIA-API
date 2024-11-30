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
        [StringLength(30)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(11)]
        public string CPF { get; set; } = string.Empty;
        
        [Required]
        public DateTime BirthDate { get; set; }
        
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
