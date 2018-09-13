using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entites
{
    public class Review
    {
        [HiddenInput(DisplayValue = true)]
        public int ReviewID { get; set; }

        //[Required]
        public DateTime Date { get; set; }

        //[Display(Name = "Автор")]
        //[Required(ErrorMessage = "Пожалуйста введите имя автора!")]
        public string Author { get; set; }

        //[DataType(DataType.MultilineText)]
        //[Display(Name = "Описание")]
        //[Required(ErrorMessage = "Пожалуйста введите опистание книги!")]
        public string Description { set; get; }

        [EmailAddress]
        public string Email { set; get; }

        public int? productId { set; get; }
        public Product product { set; get; }

        //[Range(0, 5, ErrorMessage = "Пожалуйста введите положительное название для цены!")]
        public int Rating { get; set; }
    }
}
