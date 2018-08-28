using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Model.Models
{
    public class Role : BaseEntity, IRole<int>
    {
       

        public string Name { get; set; }
    }
}
