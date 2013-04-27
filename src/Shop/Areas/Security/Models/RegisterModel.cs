using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.Areas.Security.Models
{
    public class RegisterModel
    {
        [DataType(DataType.Text)]
        [Required]
        [DisplayName("Имя пользователя")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required, DisplayName("Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Введи правильно!"), DisplayName("Подтвеждение пароля")]
        public string ConfirmPassword { get; set; }
    }
}