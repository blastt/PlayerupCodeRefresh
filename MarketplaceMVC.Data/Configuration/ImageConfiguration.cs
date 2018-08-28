using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Configuration
{
    public class ImageConfiguration : EntityTypeConfiguration<Image>
    {
        public ImageConfiguration()
        {
            ToTable("ScreenshotPathes");
            HasKey(a => a.Id);
            HasRequired(o => o.Offer).WithMany(a => a.Images).HasForeignKey(a => a.OfferId).WillCascadeOnDelete(true);

        }
    }
}
