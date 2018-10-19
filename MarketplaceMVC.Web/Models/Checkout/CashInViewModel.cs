using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Models.Checkout
{
    public class CashInViewModel
    {
        [Required]
        public decimal Price { get; set; }
    }
}