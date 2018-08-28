using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Configuration
{
    public class FeedbackConfiguration : EntityTypeConfiguration<Feedback>
    {
        public FeedbackConfiguration()
        {
            ToTable("Feedbacks");
            HasKey(a => a.Id);
            Property(f => f.Comment).IsRequired().HasMaxLength(50);
            HasRequired(f => f.Order).WithMany(o => o.Feedbacks).WillCascadeOnDelete(true);
            Property(f => f.Grade).IsRequired();


        }
    }
}
