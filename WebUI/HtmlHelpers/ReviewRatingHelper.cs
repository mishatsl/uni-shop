using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebUI.HtmlHelpers
{
    public static class ReviewRatingHelper
    {
        public static MvcHtmlString ReviewRating(this HtmlHelper html,int Rating,string css_class_of_root_element)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder tag_star = new TagBuilder("i");
            tag_star.AddCssClass("fa fa-star");
            TagBuilder tag_star_empty = new TagBuilder("i");
            tag_star_empty.AddCssClass("fa fa-star-o empty");
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass(css_class_of_root_element);
            for (int i = 1; i <= 5; i++)
            {               
               if (i <= Rating)
                { result.Append(tag_star.ToString()); }
                else
                {
                    result.Append(tag_star_empty.ToString());
                }
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}

