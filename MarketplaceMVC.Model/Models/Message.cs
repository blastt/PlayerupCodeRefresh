using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Model.Models
{
    public class Message : BaseEntity
    {
        public string MessageBody { get; set; }
        public bool FromViewed { get; set; }
        public bool ToViewed { get; set; }

        public bool SenderDeleted { get; set; }
        public bool ReceiverDeleted { get; set; }

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public UserProfile Sender { get; set; }
        public UserProfile Receiver { get; set; }

        public int DialogId { get; set; }
        public Dialog Dialog { get; set; }

    }
}
