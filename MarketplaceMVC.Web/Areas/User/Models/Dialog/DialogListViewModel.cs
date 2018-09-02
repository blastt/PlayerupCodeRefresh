using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Areas.User.Models.Dialog
{
    public class DialogListViewModel
    {
        public IEnumerable<DialogViewModel> Dialogs { get; set; }
    }
}