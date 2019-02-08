using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public AdditionalInformation AdditionalInformation { set; get; }
    }

    public class AdditionalInformation
    {
        [Key]
        [ForeignKey("Information")]
        public int Id { get; set; }

        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string ShortInfo { get; set; }

        public Information Information { get; set; }
    }
}
