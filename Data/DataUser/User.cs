using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kibernetik.Data.DataUser
{
    [Table("tblUsers")]
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required, StringLength(255)]
        public string email { get; set; }

        [Required, StringLength(255)]
        public string login { get; set; }

        [Required, StringLength(255)]
        public string password { get; set; }

        public bool email_verified { get; set; }

        [StringLength(255)]
        public string remember_token { get; set; }

    }
}
