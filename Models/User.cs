
using System.Security.Cryptography;
using System.Text;

namespace FatigueStrengthApi.Models
{
    public class User
    {
        public string Login { get; set; } = string.Empty;

        private string _passwordHash = string.Empty;

        public string Password
        {
            get => _passwordHash;
            set => _passwordHash = HashPassword(value);
        }

        public bool IsAdmin => Login == "admin";

        public bool CheckPassword(string password)
        {
            return _passwordHash == HashPassword(password);
        }

        private static string HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = MD5.HashData(bytes);

            return Convert.ToHexString(hash).ToLowerInvariant();
        }
    }
}
