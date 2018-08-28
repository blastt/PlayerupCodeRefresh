using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Configuration
{
    public class StatusLogConfiguration : EntityTypeConfiguration<StatusLog>
    {
        public StatusLogConfiguration()
        {
            ToTable("StatusLogs");
            HasKey(a => a.Id);
            HasRequired(m => m.Order).WithMany(m => m.StatusLogs).HasForeignKey(m => m.OrderId).WillCascadeOnDelete(true);
            HasRequired(m => m.NewStatus).WithMany(m => m.NewStatusLogs).HasForeignKey(m => m.NewStatusId).WillCascadeOnDelete(false); ;
            HasRequired(m => m.OldStatus).WithMany(m => m.OldStatusLogs).HasForeignKey(m => m.OldStatusId).WillCascadeOnDelete(false); ;
        }
    }
}
