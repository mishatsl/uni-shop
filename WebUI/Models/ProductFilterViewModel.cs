using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class ProductFilterViewModel
    {
        public IList<FilterOfCategory> Categories { set; get; }
        
        public int PriceMin { set; get; }

        public int PriceMax { set; get; }

        public IList<FilterOfBrand> Brands { set; get; }
    }

    public class FilterOfCategory
    {
        public bool IsChecked { set; get; }

        public string Category { set; get; }

        public int CountOfProducts { set; get; }
    }

    public class FilterOfBrand
    {
        public bool IsChecked { set; get; }

        public string Brand { set; get; }

        public int CountOfProducts { set; get; }
    }
}