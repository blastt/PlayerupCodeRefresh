using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Configuration
{
    public class DialogConfiguration : EntityTypeConfiguration<Dialog>
    {
        public DialogConfiguration()
        {
            ToTable("Dialogs");
            HasKey(a => a.Id);
            HasMany(m => m.Messages).WithRequired(m => m.Dialog).HasForeignKey(m => m.DialogId);

        }
    }
}
