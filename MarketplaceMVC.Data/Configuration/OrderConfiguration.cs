using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Configuration
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable("Orders");
            HasKey(a => a.Id);
            Property(o => o.WithmiddlemanSum).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);
            HasRequired(o => o.Offer).WithOptional(o => o.Order).WillCascadeOnDelete(true);
            HasRequired(o => o.CurrentStatus).WithMany(s => s.Orders).HasForeignKey(o => o.CurrentStatusId).WillCascadeOnDelete(true); ;

        }
    }
}
