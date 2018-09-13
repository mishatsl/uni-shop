using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Image
    {
        public int ImageID { get; set; }
       // public int ImageIndex { set; get; }

        public string alt { get; set; }

        public byte[] ImageData { set; get; }
        public string ImageMimeType { set; get; }

        public int width { set; get; }
        public int height { set; get; }

        public int? ImagesWithResolutionsId { set; get; }
        public ImagesWithResolutions ImagesWithResolutions { set; get; }
    }
}
