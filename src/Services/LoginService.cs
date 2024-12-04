using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.DTOs.Login;
using myBURGUERMANIA_API.Data;
using System.Linq;
using myBURGUERMANIA_API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace myBURGUERMANIA_API.Services
{
    public class LoginService
    {
        private readonly ApplicationDbContext _context;

        public LoginService(ApplicationDbContext context)
        {
            _context = context;
        }

        public User? Authenticate(LoginDto loginDto)
        {
            var user = _context.Users
                .Include(u => u.OrderHistory) // Incluir histórico de pedidos
                .ThenInclude(o => o.Status) // Incluir status do pedido
                .FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null || !PasswordHelper.VerifyPassword(loginDto.Password, user.Password))
            {
                return null;
            }

            // Verificar se o login já existe
            var existingLogin = _context.Logins.FirstOrDefault(l => l.Email == loginDto.Email);
            if (existingLogin == null)
            {
                // Salvar login no banco de dados
                var login = new Login
                {
                    Email = loginDto.Email,
                    Password = user.Password // Usar a senha já armazenada
                };
                _context.Logins.Add(login);
                _context.SaveChanges();
            }

            return user;
        }

        public void Logout(string email)
        {
            var login = _context.Logins.FirstOrDefault(l => l.Email == email);
            if (login != null)
            {
                _context.Logins.Remove(login);
                _context.SaveChanges();
            }
        }
    }
}