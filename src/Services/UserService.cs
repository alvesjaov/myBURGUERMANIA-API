using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.DTOs.User;
using myBURGUERMANIA_API.Data;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using myBURGUERMANIA_API.Helpers;

namespace myBURGUERMANIA_API.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private const string UserNotFound = "Usuário não encontrado.";

        public UserService(ApplicationDbContext context)
        {
            _context = context;
            if (!_context.Database.EnsureCreated())
            {
                // Banco de dados já existe, não criar novamente
            }
        }

        public User CreateUser(CreateUserDto createUserDTO)
        {
            if (_context.Users.Any(u => u.CPF == createUserDTO.CPF))
            {
                throw new ArgumentException("CPF já cadastrado.");
            }

            if (_context.Users.Any(u => u.Email == createUserDTO.Email))
            {
                throw new ArgumentException("Email já cadastrado.");
            }

            if (!PasswordValidator.IsValidPassword(createUserDTO.Password))
            {
                throw new ArgumentException("A senha deve ter pelo menos 8 caracteres, uma letra maiúscula, um número e um caractere especial.");
            }

            var user = new User
            {
                Id = IdHelper.GenerateRandomId(), // Gerar ID aleatório
                Name = createUserDTO.Name,
                Email = createUserDTO.Email,
                CPF = createUserDTO.CPF,
                BirthDate = createUserDTO.BirthDate,
                PhoneNumber = createUserDTO.PhoneNumber,
                Password = PasswordHelper.HashPassword(createUserDTO.Password) // Criptografar senha
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User? GetUser(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }
            return user;
        }

        public User GetUserByCpf(string cpf)
        {
            var user = _context.Users.FirstOrDefault(u => u.CPF == cpf);
            if (user == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }
            return user;
        }

        public bool UpdateUser(string id, UpdateUserDto updateUserDTO)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }
            user.Name = updateUserDTO.Name;
            user.Email = updateUserDTO.Email;
            user.CPF = updateUserDTO.CPF;
            user.BirthDate = updateUserDTO.BirthDate;
            user.PhoneNumber = updateUserDTO.PhoneNumber;
            if (!string.IsNullOrEmpty(updateUserDTO.Password))
            {
                if (!PasswordValidator.IsValidPassword(updateUserDTO.Password))
                {
                    throw new ArgumentException("A senha deve ter pelo menos 8 caracteres, uma letra maiúscula, um número e um caractere especial.");
                }
                user.Password = PasswordHelper.HashPassword(updateUserDTO.Password); // Criptografar senha
            }
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUser(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public static bool IsValidUser(CreateUserDto createUserDTO)
        {
            bool isValidEmail = ValidationHelper.IsValidEmail(createUserDTO.Email);
            bool isValidCPF = ValidationHelper.IsValidCPF(createUserDTO.CPF);
            bool isAdult = ValidationHelper.IsAdult(createUserDTO.BirthDate);
            bool isValidName = ValidationHelper.IsValidName(createUserDTO.Name);

            return isValidEmail && isValidCPF && isAdult && isValidName;
        }

        public static bool IsValidUser(UpdateUserDto updateUserDTO)
        {
            bool isValidEmail = ValidationHelper.IsValidEmail(updateUserDTO.Email);
            bool isValidCPF = ValidationHelper.IsValidCPF(updateUserDTO.CPF);
            bool isAdult = ValidationHelper.IsAdult(updateUserDTO.BirthDate);
            bool isValidName = ValidationHelper.IsValidName(updateUserDTO.Name);

            return isValidEmail && isValidCPF && isAdult && isValidName;
        }
    }
}