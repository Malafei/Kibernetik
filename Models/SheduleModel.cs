using Kibernetik.Data.DataShedule;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kibernetik.Models
{

    public class ShowGroupModel
    {
        public string label { get; set; }
        public int key { get; set; }
    }
    public class AddGroupModel
    {

        [Required(ErrorMessage = "Поле обовязкове")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина назви 100 символів")]
        public string nameGroup { get; set; }

    }

    public class AddSheduleModel
    {

        [Required(ErrorMessage = "Поле обовязкове")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина назви 100 символів")]
        public string nameGroup { get; set; }

        public List<Lesson> lesons { get; set; }

        public DateTime date { get; set; }

    }

    public class EditGroupModel
    {
        [Required(ErrorMessage = "Поле обовязкове")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина назви 100 символів")]
        public string New_nameGroup { get; set; }


        [Required(ErrorMessage = "Поле обовязкове")]
        public int key { get; set; }
    }

    public class EditSheduleModel
    {
        [Required(ErrorMessage = "Поле обовязкове")]
        public int key { get; set; }
        public List<Lesson> lesons { get; set; }

    }

}


