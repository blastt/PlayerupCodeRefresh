using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Configuration
{
    public class OfferConfiguration : EntityTypeConfiguration<Offer>
    {
        public OfferConfiguration()
        {
            ToTable("Offers");
            HasKey(a => a.Id);
            Property(o => o.Header).IsRequired().HasMaxLength(100);

            Property(o => o.MiddlemanPrice).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);


            Property(o => o.Discription).IsRequired().HasMaxLength(1000);
            //Property(o => o.Price).IsRequired().HasPrecision(8,2);
            Property(o => o.AccountLogin).IsRequired().HasMaxLength(50);

        }
    }
}
