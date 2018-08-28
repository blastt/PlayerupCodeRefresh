using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Model.Models
{
    public enum Emotions
    {
        Bad,
        Good
    }

    public class Feedback : BaseEntity
    {
        public Emotions Grade { get; set; }
        public string Comment { get; set; }

        public Order Order { get; set; }

        public int UserToId { get; set; }
        public UserProfile UserTo { get; set; }

        public int UserFromId { get; set; }
        public UserProfile UserFrom { get; set; }

    }
}
