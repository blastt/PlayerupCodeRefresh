using MarketplaceMVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PagedListPager(this HtmlHelper html, PageInfoViewModel pageInfo)
        {

            StringBuilder result = new StringBuilder();
            TagBuilder div = new TagBuilder("div");

            div.MergeAttribute("id", "contentPager");
            div.AddCssClass("nav-pages");
            int p = 1;
            if (pageInfo.PageNumber >= 5)
            {
                TagBuilder tag = new TagBuilder("button");
                tag.SetInnerText("1");
                tag.MergeAttribute("onclick", "submitFormByPage" + "(" + 1 + ")");
                div.InnerHtml += tag;
                TagBuilder span = new TagBuilder("span");
                span.AddCssClass(" mr-2");
                span.InnerHtml = ("...");
                div.InnerHtml += span;
                p = pageInfo.PageNumber - 3;
            }
            int to = pageInfo.TotalPages;
            bool lastBtn = false;
            if ((pageInfo.TotalPages - pageInfo.PageNumber) >= 4)
            {
                lastBtn = true;
                to = pageInfo.PageNumber + 3;
            }
            for (int i = p; i <= to; i++)
            {


                TagBuilder tag = new TagBuilder("button");
 
                tag.SetInnerText(i.ToString());
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("active");
                }
                else
                {
                    tag.MergeAttribute("onclick", "submitFormByPage" + "(" + i + ")");
                }

                div.InnerHtml += tag;

            }
            if (lastBtn)
            {
                TagBuilder tag = new TagBuilder("button");
                tag.SetInnerText(pageInfo.TotalPages.ToString());
                tag.MergeAttribute("onclick", "submitFormByPage" + "(" + pageInfo.TotalPages + ")");
                TagBuilder span = new TagBuilder("span");
                span.AddCssClass(" mr-2");
                span.InnerHtml = ("...");
                div.InnerHtml += span;
                div.InnerHtml += tag;


            }
            result.Append(div.ToString());
            return MvcHtmlString.Create(result.ToString());
        }
    }
}