using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Areas.Admin.Models.Game
{
    public class CreateGameViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int Rank { get; set; }
    }
}