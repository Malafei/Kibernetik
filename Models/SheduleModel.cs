using System.ComponentModel.DataAnnotations;

namespace Kibernetik.Models
{

    public class ShowGroupModel
    {
        public string nameGroup { get; set; }
    }
    public class AddGroupModel
    {

        [Required(ErrorMessage = "Поле обовязкове")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина назви 100 символів")]
        public string nameGroup { get; set; }

    }
}


