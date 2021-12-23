using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kibernetik.Models
{
    public class AccountModel
    {
        public class RegisterModel
        {
            [Required(ErrorMessage = "Required")]
            [EmailAddress(ErrorMessage = "Required")]
            public string email { get; set; }

            [Required(ErrorMessage = "Required")]
            [MinLength(6, ErrorMessage = "The password must contain at least 6 characters")]
            [MaxLength(20, ErrorMessage = "The password must contain a maximum of 20 characters")]
            [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$",
                ErrorMessage = "Incorrect password.")]
            public string password { get; set; }

            [Required(ErrorMessage = "Required")]
            [Compare("password", ErrorMessage = "Password and password confirmation must match")]
            public string confirmPassword { get; set; }

        }
    }
}
