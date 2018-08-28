using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Configuration
{
    public class TransactionConfiguration : EntityTypeConfiguration<Transaction>
    {
        public TransactionConfiguration()
        {
            ToTable("Transactions");
            HasKey(a => a.Id);
            HasRequired(t => t.Order).WithMany(o => o.Transactions).HasForeignKey(t => t.OrderId).WillCascadeOnDelete(true);

            HasRequired(t => t.Receiver).WithMany(o => o.TransactionsAsReceiver).HasForeignKey(t => t.ReceiverId).WillCascadeOnDelete(false);
            HasRequired(t => t.Sender).WithMany(o => o.TransactionsAsSender).HasForeignKey(t => t.SenderId).WillCascadeOnDelete(false);

        }
    }
}
