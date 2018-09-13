using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entites
{
    public class Book
    {
        [HiddenInput(DisplayValue = true)]
        public int BookID { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage ="Пожалуйста введите название!")]
        public string Name { get; set; }

        [Display(Name = "Автор")]
        [Required(ErrorMessage = "Пожалуйста введите имя автора!")]
        public string Author { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста введите опистание книги!")]
        public string Description { set; get; }

        [Display(Name = "Жанр")]
        [Required(ErrorMessage = "Пожалуйста введите жанр!")]
        public string Genre { set; get; }

        [Display(Name = "Цена (руб)")]
        [Required]
        [Range(0.01,double.MaxValue, ErrorMessage = "Пожалуйста введите положительное название для цены!")]
        public decimal Price { set; get; }

        public byte[] ImageData { set; get; }
        public string ImageMimeType { set; get; }
    }
}
