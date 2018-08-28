using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Configuration
{
    public class BillingConfiguration : EntityTypeConfiguration<Billing>
    {
        public BillingConfiguration()
        {
            ToTable("Billings");
            HasKey(a => a.Id);
            HasRequired(b => b.User).WithMany(u => u.Billings).HasForeignKey(b => b.UserId);
        }
    }
}
