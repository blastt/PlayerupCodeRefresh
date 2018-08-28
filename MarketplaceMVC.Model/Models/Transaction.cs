using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Model.Models
{
    public class Transaction : BaseEntity
    {
        public decimal Amount { get; set; }

        public int SenderId { get; set; }
        public UserProfile Sender { get; set; }

        public int ReceiverId { get; set; }
        public UserProfile Receiver { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
