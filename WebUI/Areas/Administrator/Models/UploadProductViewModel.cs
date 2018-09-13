using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Administrator.Models
{
    public class UploadProductViewModel
    {
        public Product Product { set; get; }
        public List<State> States { set; get; }
    }
}