using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Areas.Administrator.Models
{
    public class ImageUploadViewModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int ProductID { set; get; }
        public State State { set; get; }
        public Domain.Entites.Image Image {set; get;}
    }

    public enum State
    {
        update,
        delete,
        add,
        saved,
        doNothing
    }
}