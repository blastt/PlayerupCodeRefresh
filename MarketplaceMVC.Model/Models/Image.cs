using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Model.Models
{
    public class Image : BaseEntity
    {
        public string Value { get; set; }

        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
