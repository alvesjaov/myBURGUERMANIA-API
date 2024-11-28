using System.ComponentModel.DataAnnotations;

namespace myBURGUERMANIA_API.Models
{
    public class User
    {
        public int Id { get; set; }
        
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
        public DateOnly BirthDate { get; set; }
        
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
