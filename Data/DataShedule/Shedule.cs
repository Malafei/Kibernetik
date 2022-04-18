using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kibernetik.Data.DataShedule
{
    public class Shedule
    {
        [Table("tblShedules")]
        public class User
        {
            [Key]
            public int id { get; set; }

            [Required, StringLength(255)]
            public string name_group { get; set; }

            public ICollection<Lesson> lessons { get; set; }

            public DateTime date { get; set; }








        }
    }
}
