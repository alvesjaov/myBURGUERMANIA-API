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
            return user;
        }
    }
}