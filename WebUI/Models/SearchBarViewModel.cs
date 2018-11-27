using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class SearchBarViewModel
    {
        public string searchParam { set; get; }
        public IEnumerable<string> Categories { set; get; }
    }
}