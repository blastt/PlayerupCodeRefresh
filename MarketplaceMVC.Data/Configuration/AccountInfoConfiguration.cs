using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Configuration
{
    public class AccountInfoConfiguration : EntityTypeConfiguration<AccountInfo>
    {
        public AccountInfoConfiguration()
        {
            ToTable("AccountInfos");
            HasKey(a => a.Id);
            HasRequired(o => o.Order).WithMany(a => a.AccountInfos).HasForeignKey(a => a.OrderId);

        }
    }
}
