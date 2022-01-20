using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kibernetik.Models
{
        public class RegisterModel
        {
            [Required(ErrorMessage = "Вкажіть пошту")]
            [EmailAddress(ErrorMessage = "Не коректно вказана пошта")]
            public string email { get; set; }

            [Required(ErrorMessage = "Вкажіть пароль.")]
            [MinLength(8, ErrorMessage = "Пароль має містить мінімум 8 символів.")]
            [MaxLength(20, ErrorMessage = "Пароль має містить максимум 20 символів.")]
            [RegularExpression("^[A-Za-z0-9]+$",
                ErrorMessage = "Пароль має містить латинські символи.")]
            public string password { get; set; }

            [Required(ErrorMessage = "Повторіть пароль")]
            [Compare("password", ErrorMessage = "Пароль та повтор паролю повині співпадати.")]
            public string confirmPassword { get; set; }

        }


        public class LoginModel
        {
            [Required(ErrorMessage = "Вкажіть пошту")]
            [EmailAddress(ErrorMessage = "Не коректно вказана пошта")]
            public string email { get; set; }

            [Required(ErrorMessage = "Вкажіть пароль.")]
            [MinLength(8, ErrorMessage = "Пароль має містить мінімум 8 символів.")]
            [MaxLength(20, ErrorMessage = "Пароль має містить максимум 20 символів.")]
            [RegularExpression("^[A-Za-z0-9]+$",
                ErrorMessage = "Пароль має містить латинські символи.")]
            public string password { get; set; }
        }
}
