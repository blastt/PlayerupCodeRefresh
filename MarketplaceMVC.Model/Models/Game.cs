using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Model.Models
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public string ImagePath { get; set; }

        public int Rank { get; set; } // порядковый номер среди рангов

        public ICollection<Offer> Offers { get; set; }
    }
}
