using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Model.Models
{
    public class AccountInfo : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public string AdditionalInformation { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
