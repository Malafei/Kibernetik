using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kibernetik.Data.DataNews
{
    [Table("tblNews")]
    public class News
    {
        [Key]
        public int id { get; set; }


        [Required, StringLength(255)]
        public string name { get; set; }

        [Required, StringLength(255)]
        public string image { get; set; }

        [Required, StringLength(255)]
        public string description { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
