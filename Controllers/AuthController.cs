using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FatigueStrengthApi.Models;

namespace FatigueStrengthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public struct LoginData
        {
            public string login { get; set; }
            public string password { get; set; }
        }

        [HttpPost]
        public object GetToken([FromBody] LoginData ld)
        {
            var user = SharedData.Users.FirstOrDefault(
                u => u.Login == ld.login
            );

            if (user == null || !user.CheckPassword(ld.password))
            {
                Response.StatusCode = 401;
                return new { message = "wrong login/password" };
            }

            return AuthOptions.GenerateToken(user.IsAdmin);
        }

        [HttpGet("users")]
        public List<User> GetUsers()
        {
            return SharedData.Users;
        }

        [HttpGet("token")]
        public object GetToken()
        {
            return AuthOptions.GenerateToken();
        }

        [HttpGet("token/secret")]
        public object GetAdminToken()
        {
            return AuthOptions.GenerateToken(true);
        }
    }
}
