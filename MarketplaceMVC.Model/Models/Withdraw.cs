using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Model.Models
{
    public class Withdraw : BaseEntity
    {
        public string PaywayName { get; set; }
        public string Details { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }

        public int UserId { get; set; }
        public UserProfile User { get; set; }
    }
}
