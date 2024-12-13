using System.ComponentModel.DataAnnotations;

namespace AuthApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Ім'я користувача є обов'язковим.")]
        [StringLength(50, ErrorMessage = "Ім'я користувача має бути не більше 50 символів.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Пароль є обов'язковим.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль має бути від 6 до 100 символів.")]
        public string Password { get; set; }
    }
}