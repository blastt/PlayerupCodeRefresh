using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Model.Models
{
    public class UserLogin : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
