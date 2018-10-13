using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Areas.Admin.Models.UserProfile
{
    public class UserProfileListViewModel
    {
        public IEnumerable<UserProfileViewModel> UserProfiles { get; set; }
    }
}