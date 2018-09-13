using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entites
{
    public class Product
    {
        [HiddenInput(DisplayValue = true)]
        public int ProductID { get; set; }

        //[Display(Name = "Название")]
        //[Required(ErrorMessage = "Пожалуйста введите название!")]
        public string Name { get; set; }

        //[Display(Name = "Название")]
        //[Required(ErrorMessage = "Пожалуйста введите название!")]
        public string Сategory { get; set; }

        //[Display(Name = "Название")]
        //[Required(ErrorMessage = "Пожалуйста введите название!")]
        public string Brand { get; set; }

        //[DataType(DataType.MultilineText)]
        //[Display(Name = "Описание")]
        //[Required(ErrorMessage = "Пожалуйста введите опистание книги!")]
        public string Description { set; get; }

        public string Color { set; get; }

        //[Display(Name = "Цена (руб)")]
        //[Required]
        //[Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста введите положительное название для цены!")]
        public decimal Price { set; get; }

        //Business logic
        //[Display(Name = "Цена (руб)")]
        //[Required]
        //[Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста введите положительное название для цены!")]
        public decimal PreviousPrice { set; get; }

        //Business logic
        //[Required]
        public bool InStock { get; set; }

        //[Range(0, 5, ErrorMessage = "Пожалуйста введите положительное название для цены!")]
        public int ProductRating { set; get; }

        public string  Details { set; get; }

        //business layer
        public string feature { set; get; }

        //business layer
        public bool TopSelling { set; get; }

        //business layer
        public bool New { get; set; }

        public float average_rating { get; set; }

        public ICollection<Review>  Reviews { set; get; } 

        public Product()
        {
            Reviews = new List<Review>();
            ImagesWithResolutions = new List<ImagesWithResolutions>();
        }

        //public int? ImagesID { set; get; }
        public int MainImageIndex { set; get; }
        public virtual ICollection<ImagesWithResolutions> ImagesWithResolutions { set; get; }

        //public byte[] ImageData { set; get; }
        //public string ImageMimeType { set; get; }
    }
}
