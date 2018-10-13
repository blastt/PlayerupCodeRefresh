using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Areas.Admin.Models.UserProfile
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsBanned { get; set; }
        public int Rating { get; set; }
        public decimal Balance { get; set; }
        public string LockoutReason { get; set; }
        public int PositiveFeedbackCount{ set; get; }
        public int NegativeFeedbackCount{ set; get; }
        public string Avatar32Path { get; set; }
    }
}