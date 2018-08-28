using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Model.Models
{
    public class UserClaim : BaseEntity
    {

        public int UserId { get; set; }
        public User User { get; set; }

        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
