using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kibernetik.Models
{
    public class UserModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Вкажіть пошту")]
        [EmailAddress(ErrorMessage = "Не коректно вказана пошта")]
        public string email { get; set; }

        [Required(ErrorMessage = "Вкажіть логін")]
        public string login { get; set; }
    }

    public class AddNewUser
    {
        [Required(ErrorMessage = "Вкажіть пошту")]
        [EmailAddress(ErrorMessage = "Не коректно вказана пошта")]
        public string email { get; set; }

        [Required(ErrorMessage = "Вкажіть логін")]
        public string login { get; set; }
        [Required(ErrorMessage = "Вкажіть пароль")]
        public string password { get; set; }
    }

    public class SearchUser
    {
        public string word { get; set; }
    }
}
