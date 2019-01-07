using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entites
{
    public class Information
    {
        public int Id { set; get; }

        public string Head { set; get; }

        [AllowHtml]
        public string HTMLContent { set; get; }
    }
}
