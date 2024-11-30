using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace myBURGUERMANIA_API.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length >= 3;
        }
        
        public static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[\w\-\.]+@([\w-]+\.)+[\w-]{2,}$");
            return emailRegex.IsMatch(email);
        }

        public static bool IsValidCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
                return false;

            var cpfArray = cpf.Select(c => int.Parse(c.ToString())).ToArray();

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += cpfArray[i] * (10 - i);

            int remainder = (sum * 10) % 11;
            if (remainder == 10) remainder = 0;

            if (remainder != cpfArray[9])
                return false;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += cpfArray[i] * (11 - i);

            remainder = (sum * 10) % 11;
            if (remainder == 10) remainder = 0;

            return remainder == cpfArray[10];
        }

        public static bool IsAdult(DateTime birthDate)
        {
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
            return age >= 18;
        }

    }
}