using FatigueStrengthApi.Models;
using FatigueStrengthApi.Services;
using Microsoft.AspNetCore.Mvc;

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

        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }

        // POST: api/Auth
        [HttpPost]
        public IActionResult GetToken([FromBody] LoginData ld)
        {
            try
            {
                var token = _service.Login(ld.login, ld.password);
                return Ok(token);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(
                    new { message = "wrong login/password" });
            }
        }

        // GET: api/Auth/users
        [HttpGet("users")]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(_service.GetUsers());
        }

        // GET: api/Auth/token
        [HttpGet("token")]
        public IActionResult GetToken()
        {
            return Ok(_service.GetGuestToken());
        }

        // GET: api/Auth/token/secret
        [HttpGet("token/secret")]
        public IActionResult GetAdminToken()
        {
            return Ok(_service.GetAdminToken());
        }
    }
}
