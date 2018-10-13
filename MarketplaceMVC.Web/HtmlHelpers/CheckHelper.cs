using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.HtmlHelpers
{
    public static class CheckHelper
    {
        public static MvcHtmlString Check(this HtmlHelper html, bool b)
        {
            StringBuilder result = new StringBuilder();

            TagBuilder tag = new TagBuilder("i");
            tag.AddCssClass("far");
            if (b)
            {
                tag.AddCssClass("fa-check-circle text-success");
            }
            else
            {
                tag.AddCssClass("fa-times-circle text-danger");
            }
            result.Append(tag);
            return new MvcHtmlString(result.ToString());
        }
    }
}