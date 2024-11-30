using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.DTOs.User;
using myBURGUERMANIA_API.Helpers;
using myBURGUERMANIA_API.Data;
using System.Globalization;

namespace myBURGUERMANIA_API.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

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

            var user = new User
            {
                Id = IdHelper.GenerateRandomId(), // Gerar ID aleatório
                Name = createUserDTO.Name,
                Email = createUserDTO.Email,
                CPF = createUserDTO.CPF,
                BirthDate = createUserDTO.BirthDate,
                PhoneNumber = createUserDTO.PhoneNumber
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User? GetUser(string id)
        {
            return _context.Users.Find(id);
        }

        public bool UpdateUser(string id, UpdateUserDto updateUserDTO)
        {
            var user = _context.Users.Find(id);
            if (user == null) return false;

            user.Name = updateUserDTO.Name;
            user.Email = updateUserDTO.Email;
            user.CPF = updateUserDTO.CPF;
            user.BirthDate = updateUserDTO.BirthDate;
            user.PhoneNumber = updateUserDTO.PhoneNumber;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUser(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return false;

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

            Console.WriteLine($"Validating User: Email={isValidEmail}, CPF={isValidCPF}, IsAdult={isAdult}, Name={isValidName}");

            return isValidEmail && isValidCPF && isAdult && isValidName;
        }

        public static bool IsValidUser(UpdateUserDto updateUserDTO)
        {
            bool isValidEmail = ValidationHelper.IsValidEmail(updateUserDTO.Email);
            bool isValidCPF = ValidationHelper.IsValidCPF(updateUserDTO.CPF);
            bool isAdult = ValidationHelper.IsAdult(updateUserDTO.BirthDate);
            bool isValidName = ValidationHelper.IsValidName(updateUserDTO.Name);

            Console.WriteLine($"Validating User: Email={isValidEmail}, CPF={isValidCPF}, IsAdult={isAdult}, Name={isValidName}");

            return isValidEmail && isValidCPF && isAdult && isValidName;
        }
    }
}