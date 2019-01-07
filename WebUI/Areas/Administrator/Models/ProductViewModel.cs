using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Areas.Administrator.Models
{
    public class ProductViewModel
    {
        public PagingInfo PagingInfo { set; get; }
        public IEnumerable<Product> Products { set; get; }
    }
}