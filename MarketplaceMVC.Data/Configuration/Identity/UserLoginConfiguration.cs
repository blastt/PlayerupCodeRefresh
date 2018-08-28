using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Configuration
{
    internal class UserLoginConfiguration : EntityTypeConfiguration<UserLogin>
    {
        public UserLoginConfiguration()
        {
            ToTable("UserLogins");
            HasKey(c => new
            {
                c.LoginProvider,
                c.ProviderKey,
                c.UserId
            });
        }
    }
}
