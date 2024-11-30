using System.Text.RegularExpressions;

namespace myBURGUERMANIA_API.Helpers
{
    public static class PasswordValidator
    {
        public static bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return false;

            if (!Regex.IsMatch(password, @"[\W_]"))
                return false;

            if (!Regex.IsMatch(password, @"[0-9]"))
                return false;

            return true;
        }
    }
}