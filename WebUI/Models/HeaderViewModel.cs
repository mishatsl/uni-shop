using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class HeaderViewModel
    {
        public Cart cart { get; set; }
        public IEnumerable<string> Categories { set; get; }
        public AdditionalInformation AdditionalInformation { set; get; }
       // public IEnumerable<ProductWidgetCartModel> ProductWidgetCartModels { set; get; }
    }
}