using System.ComponentModel.DataAnnotations;

namespace myBURGUERMANIA_API.DTOs.User
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty; // Definir comprimento máximo
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
