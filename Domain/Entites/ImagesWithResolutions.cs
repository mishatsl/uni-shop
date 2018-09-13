using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ImagesWithResolutions
    {

        public int ImagesWithResolutionsID { set; get; }
        public ImagesWithResolutions()
        {
            Images = new List<Image>();
        }
        public ICollection<Image> Images { set; get; }
        public int? productId { set; get; }
        public Product product { set; get; }
    }
}
