using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Areas.Administrator.Models
{
    public class FilterSideBarViewModel
    {
        public IEnumerable<string> Categories { set; get; }
        public IEnumerable<string> Brands { set; get; }
    }
}