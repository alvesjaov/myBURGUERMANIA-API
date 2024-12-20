
using System.ComponentModel.DataAnnotations;

namespace myBURGUERMANIA_API.Models
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}