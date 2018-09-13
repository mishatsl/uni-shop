using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entites;

namespace WebUI.Models
{
    public class MiniProductWidgetViewModel { 

        public int ProductID { set; get; }
        public string Name { set; get; }
        public string Category { set; get; }
        public Image Image { set; get; }
        public decimal Price { set; get; }
        public decimal OldPrice { set; get; }
    }
}