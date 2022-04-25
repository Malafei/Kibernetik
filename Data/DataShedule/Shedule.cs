using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kibernetik.Data.DataShedule
{
    [Table("tblShedules")]
    public class Shedule
    {
            [Key]
            public int id { get; set; }

            [Required, StringLength(255)]
            public string name_group { get; set; }

            public ICollection<Lesson> lessons { get; set; }



    }
}
