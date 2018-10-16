using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Web.Areas.User.Models.Message;
using MarketplaceMVC.Web.Areas.User.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Areas.User.Models.Dialog
{
    public class DialogViewModel
    {
        public int Id { get; set; }

        public int CountOfNewMessages { get; set; }

        public int CreatorId { get; set; }
        public UserProfileViewModel Creator { get; set; }

        public int CompanionId { get; set; }
        public UserProfileViewModel Companion { get; set; }
        public ICollection<MessageViewModel> Messages { get; set; }
    }
}