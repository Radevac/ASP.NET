using System.ComponentModel.DataAnnotations;

namespace AuthApp.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ім'я користувача є обов'язковим.")]
        [StringLength(50, ErrorMessage = "Ім'я користувача має бути не більше 50 символів.")]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [StringLength(20)]
        public string Role { get; set; } = "User"; // Роль за замовчуванням
    }
}