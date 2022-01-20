using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kibernetik.Models
{
    public class ItemNewsModel{
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string date { get; set; }
    }

    public class AddNewsModel
    {

        [Required(ErrorMessage = "Поле обовязкове")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина заголовку 100 символів")]
        public string name { get; set; }

        public IFormFile image { get; set; }


        [Required(ErrorMessage = "Поле обовязкове")]
        public string description { get; set; }
    }

    public class NewsSaveViewModel
    {

        public int id { get; set; }


        [Required(ErrorMessage = "Поле обовязкове")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина заголовку 100 символів")]
        public string name { get; set; }


        [Required(ErrorMessage = "Поле обовязкове")]
        public string description { get; set; }


        public IFormFile image { get; set; }

    }
}
