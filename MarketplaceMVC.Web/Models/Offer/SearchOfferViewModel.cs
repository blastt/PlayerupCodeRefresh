using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Models.Offer
{
    public class SearchOfferViewModel
    {
        public string Game { get; set; }
        public string SearchString { get; set; }
        public bool SerchInDescription { get; set; }
        public bool OnlineOnly { get; set; }
        public decimal PriceFrom { get; set; }
        public string SortBy { get; set; }
        public decimal PriceTo { get; set; }
        public bool PersonalAccount { get; set; }
        public bool IsBanned { get; set; }
    }
}