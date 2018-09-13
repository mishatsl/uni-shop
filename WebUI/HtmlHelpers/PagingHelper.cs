using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.HtmlHelpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            TagBuilder tag_ul = new TagBuilder("ul");
            tag_ul.AddCssClass("store-pagination");
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPage && i<=4; i++)
            {
                TagBuilder tag_li = new TagBuilder("li");
                TagBuilder tag_a = new TagBuilder("a");
                tag_a.MergeAttribute("href", pageUrl(i));
                tag_a.InnerHtml = i.ToString();
                if (i==pagingInfo.CurrentPage)
                {
                    tag_a.AddCssClass("active");
                }
                tag_li.InnerHtml = "tag_a";
                result.Append(tag_li.ToString());
            }
            if(pagingInfo.TotalPage>4)

            tag_ul.InnerHtml = result.ToString();
           return  MvcHtmlString.Create(tag_ul.ToString());
        }
    }
}