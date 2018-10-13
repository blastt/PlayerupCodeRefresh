using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Models.Checkout
{
    public class CheckoutViewModel
    {
        public int OfferId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string Game { get; set; }
        public string OfferHeader { get; set; }
        public bool SellerPaysMiddleman { get; set; }
        public bool UserCanPayWithBalance { get; set; }
        [DataType(DataType.Currency)]
        public decimal MiddlemanPrice { get; set; }

        [DataType(DataType.Currency)]
        public decimal OrderSum { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
    }
}