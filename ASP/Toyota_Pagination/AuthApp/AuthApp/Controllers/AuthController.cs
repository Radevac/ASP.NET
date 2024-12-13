using Microsoft.AspNetCore.Mvc;
using AuthApp.Helpers;
using AuthApp.Data;
using AuthApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (await _context.Users.AnyAsync(u => u.Username == model.Username))
            {
                return BadRequest(new { Message = "Користувач з таким іменем вже існує." });
            }

            var hasher = new PasswordHasher<UserModel>();
          var user = new UserModel
            {
                Username = model.Username,
                Role = model.Role
            };

            
            user.PasswordHash = hasher.HashPassword(user, model.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Реєстрація успішна!" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Гіпотетична перевірка логіна і пароля
            if (model.Username == "admin" && model.Password == "password")
            {
                var jwtHelper = new JwtHelper(
                    _configuration["Jwt:Key"],
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"]
                );

                var token = jwtHelper.GenerateToken(model.Username, "Admin");
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials");
        }
    }

    // Модель для входу
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    // Модель для реєстрації
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
