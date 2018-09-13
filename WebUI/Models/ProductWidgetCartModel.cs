using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class ProductWidgetCartModel
    {
        public int ProductID { set; get; }
        public string Name { set; get; }
        public string Category { set; get; }
        public Image Image { set; get; }
        public decimal Price { set; get; }
        public decimal OldPrice { set; get; }
        public CartLine Line { set; get; }
    }
}