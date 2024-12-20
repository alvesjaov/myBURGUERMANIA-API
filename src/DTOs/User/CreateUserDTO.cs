using System.ComponentModel.DataAnnotations;

namespace myBURGUERMANIA_API.DTOs.User
{
    public class CreateUserDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // Novo campo de senha
    }
}