using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.UnitTests
{
    public class TestContext : DbContext
    {
        public TestContext()
            : base("Name=TestContext")
        {

        }

        public TestContext(bool enableLazyLoading, bool enableProxyCreation)
            : base("Name=TestContext")
        {
            Configuration.ProxyCreationEnabled = enableProxyCreation;
            Configuration.LazyLoadingEnabled = enableLazyLoading;
        }

        public TestContext(DbConnection connection)
            : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Suppress code first model migration check         
            Database.SetInitializer<TestContext>(new AlwaysCreateInitializer());

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public void Seed(TestContext Context)
        {
            var game1 = new Game { Id = 1, Value = "csgo" };
            var game2 = new Game { Id = 2, Value = "dota2" };
            var game3 = new Game { Id = 3, Value = "wow" };
            var game4 = new Game { Id = 4, Value = "lol" };
            var dialog1 = new Dialog()
            {

            };
            var dialog2 = new Dialog()
            {

            };
            var dialog3 = new Dialog()
            {

            };
            var dialog4 = new Dialog()
            {

            };


            var user1 = new User
            {
                UserName = "Tom",
                UserProfile = new UserProfile()
                {
                    DialogsAsCreator = new List<Dialog>() { dialog1 },
                    DialogsAsСompanion = new List<Dialog>() { dialog4 },
                }

            };
            var user2 = new User
            {
                UserName = "Andrey",
                UserProfile = new UserProfile()
                {
                    DialogsAsСompanion = new List<Dialog>() { dialog1 },
                    DialogsAsCreator = new List<Dialog>() { dialog2 }
                }
            };
            var user3 = new User
            {
                UserName = "Dmitry",
                UserProfile = new UserProfile()
                {
                    DialogsAsСompanion = new List<Dialog>() { dialog2 },
                    DialogsAsCreator = new List<Dialog>() { dialog3 },
                }
            };
            var user4 = new User
            {
                UserName = "Ivan",
                UserProfile = new UserProfile()
                {
                    DialogsAsСompanion = new List<Dialog>() { dialog3 },
                    DialogsAsCreator = new List<Dialog>() { dialog4 },
                }
            };

            var mess1 = new Message { MessageBody = "Hello mate, how are you?", Sender = user1.UserProfile, Receiver = user2.UserProfile };
            var mess2 = new Message { MessageBody = "Hi, I am fine?", Sender = user2.UserProfile, Receiver = user1.UserProfile };

            var mess3 = new Message { MessageBody = "Hello mate, how are you? 2", Sender = user2.UserProfile, Receiver = user3.UserProfile };
            var mess4 = new Message { MessageBody = "Hi, I am fine? 2", Sender = user3.UserProfile, Receiver = user2.UserProfile };

            var mess5 = new Message { MessageBody = "Hello mate, how are you? 3", Sender = user3.UserProfile, Receiver = user4.UserProfile };
            var mess6 = new Message { MessageBody = "Hi, I am fine? 3", Sender = user4.UserProfile, Receiver = user3.UserProfile };

            var mess7 = new Message { MessageBody = "Hello mate, how are you? 4", Sender = user4.UserProfile, Receiver = user1.UserProfile };
            var mess8 = new Message { MessageBody = "Hi, I am fine? 4", Sender = user1.UserProfile, Receiver = user4.UserProfile };

            dialog1.Messages.Add(mess1);
            dialog1.Messages.Add(mess2);

            dialog2.Messages.Add(mess3);
            dialog2.Messages.Add(mess4);

            dialog3.Messages.Add(mess5);
            dialog3.Messages.Add(mess6);

            dialog4.Messages.Add(mess7);
            dialog4.Messages.Add(mess8);

            Context.Users.AddRange(new List<User> { user1, user2, user3, user4 });
            Context.SaveChanges();
        }

        public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<TestContext>
        {
            protected override void Seed(TestContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

        public class CreateInitializer : CreateDatabaseIfNotExists<TestContext>
        {
            protected override void Seed(TestContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

        public class AlwaysCreateInitializer : DropCreateDatabaseAlways<TestContext>
        {
            protected override void Seed(TestContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }
    }
}
