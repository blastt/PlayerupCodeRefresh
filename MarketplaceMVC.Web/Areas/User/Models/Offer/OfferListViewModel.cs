using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Areas.User.Models.Offer
{
    public class OfferListViewModel
    {
        public IEnumerable<OfferViewModel> Offers { get; set; } = new List<OfferViewModel>();
        public int CountOfActive { get; set; }
        public int CountOfInactive { get; set; }
        public int CountOfClosed { get; set; }
    }
}