using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kibernetik.Data.DataShedule
{
        [Table("tblLesson")]
        public class Lesson
        {
            [Key]
            public int id { get; set; }

            [Required, StringLength(255)]
            public string name_lesson { get; set; }

            [Required, StringLength(255)]
            public string teather { get; set; }

            [Required, StringLength(255)]
            public string type { get; set; }

            [Required, StringLength(255)]
            public string classroom { get; set; }

            [Required]
            public DateTime time { get; set; }

            public Shedule shedule { get; set; }

        }
}
