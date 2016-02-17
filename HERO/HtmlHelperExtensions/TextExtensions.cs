using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HERO.Models.Objects;

namespace HERO.HtmlHelperExtensions
{
    public static class TextExtensions
    {
        public static MvcHtmlString WODScore(this HtmlHelper helper, WODScoring scoring, double score)
        {
            if (scoring == WODScoring.TotalTime)
            {
                TimeSpan time = TimeSpan.FromSeconds(score);
                string text = time.ToString(@"mm\:ss");
                return new MvcHtmlString(text);
            } else
            {
                return new MvcHtmlString(score.ToString());
            }
        }
    }
}