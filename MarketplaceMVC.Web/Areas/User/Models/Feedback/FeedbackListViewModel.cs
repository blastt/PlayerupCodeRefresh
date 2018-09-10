using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Areas.User.Models.Feedback
{
    public class FeedbackListViewModel
    {
        public IEnumerable<FeedbackViewModel> Feedbacks { get; set; }
        public int CountOfPositive { get; set; }
        public int CountOfNegative { get; set; }
    }
}