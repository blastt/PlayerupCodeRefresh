using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Areas.Admin.Models.UserProfile
{
    public class LockUserViewModel
    {
        public int UserId { get; set; }
        public string LockoutReason { get; set; }
    }
}