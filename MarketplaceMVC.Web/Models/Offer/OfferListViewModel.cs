using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Models.Offer
{
    public class OfferListViewModel
    {
        public IEnumerable<OfferViewModel> Offers { get; set; } = new List<OfferViewModel>();
        public IList<SelectListItem> Games { get; set; } = new List<SelectListItem>();
    }
}