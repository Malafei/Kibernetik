using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Kibernetik.Data.DataShedule
{
    [Table("tblGroups")]
    public class Group
    {
        [Key]
        public int id { get; set; }

        [Required, StringLength(255)]
        public string name_group { get; set; }
    
    }
}
