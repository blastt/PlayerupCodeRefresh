using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Model.Models
{
    public class Billing : BaseEntity
    {
        public decimal Amount { get; set; }

        public int UserId { get; set; }
        public UserProfile User { get; set; }
    }
}
