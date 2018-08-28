using MarketplaceMVC.Data.Configuration;
using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.EF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection") { }

        private static void Magic()
        {
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            if (type == null)
                throw new Exception("Do not remove, ensures static reference to System.Data.Entity.SqlServer");
        }

        #region Identity
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        #endregion

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<AccountInfo> AccountInfos { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<StatusLog> StatusLogs { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            #region Identity
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new UserClaimConfiguration());
            modelBuilder.Configurations.Add(new UserLoginConfiguration());
            #endregion

            modelBuilder.Configurations.Add(new UserProfileConfiguration());
            modelBuilder.Configurations.Add(new OfferConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new FeedbackConfiguration());
            modelBuilder.Configurations.Add(new MessageConfiguration());
            modelBuilder.Configurations.Add(new GameConfiguration());
            modelBuilder.Configurations.Add(new AccountInfoConfiguration());
            modelBuilder.Configurations.Add(new OrderStatusConfiguration());
            modelBuilder.Configurations.Add(new DialogConfiguration());
            modelBuilder.Configurations.Add(new StatusLogConfiguration());
            modelBuilder.Configurations.Add(new BillingConfiguration());
            modelBuilder.Configurations.Add(new TransactionConfiguration());
            modelBuilder.Configurations.Add(new ImageConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}
