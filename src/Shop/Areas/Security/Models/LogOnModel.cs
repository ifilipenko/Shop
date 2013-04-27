using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.Areas.Security.Models
{
    public class LogOnModel
    {
        [Required]
        [DisplayName("Имя пользователя")]
        public string Username { get; set; }

        [Required]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [DisplayName("Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}