using System.Linq;
using FatigueStrengthApi.Models;

namespace FatigueStrengthApi.Services
{
    public class AuthService
    {
        public object Login(string login, string password)
        {
            var user = SharedData.Users.FirstOrDefault(
                u => u.Login == login
            );

            if (user == null || !user.CheckPassword(password))
            {
                throw new UnauthorizedAccessException(
                    "wrong login/password");
            }

            return AuthOptions.GenerateToken(user.IsAdmin);
        }

        public List<User> GetUsers()
        {
            return SharedData.Users;
        }

        public object GetGuestToken()
        {
            return AuthOptions.GenerateToken();
        }

        public object GetAdminToken()
        {
            return AuthOptions.GenerateToken(true);
        }
    }
}
