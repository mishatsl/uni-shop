using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class ProductSlideViewModel
    {
        public IEnumerable<Product> Products { set; get; }
        public int TotalProductsInSlide { set; get; }
    }
}