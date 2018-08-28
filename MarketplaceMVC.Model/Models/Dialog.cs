using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Model.Models
{
    public class Dialog : BaseEntity
    {

        public int CreatorId { get; set; }
        public UserProfile Creator { get; set; }

        public int CompanionId { get; set; }
        public UserProfile Companion { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();

    }
}
