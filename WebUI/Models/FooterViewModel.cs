using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class FooterViewModel
    {
        public IEnumerable<string> Categoreis { set; get; }
        public IEnumerable<string> Informations { set; get; }
        public AdditionalInformation AdditionalInformation { set; get; }
    }
}