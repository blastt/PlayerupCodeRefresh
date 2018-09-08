using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.EF
{
    public class MarketplaceMVCSeedData : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {

            GetUsers().ForEach(c => context.Users.Add(c));

            context.SaveChanges();
        }

        private static List<User> GetUsers()
        {
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

            return new List<User>() { user1, user2, user3, user4 };
        }


        private static List<UserProfile> GetUserProfiles()
        {
            return new List<UserProfile>
            {
                new UserProfile()
                {


                },
                new UserProfile()
                {

                },
                new UserProfile()
                {


                },
                new UserProfile()
                {

                }
            };
        }

        private static List<Message> GetMessages()
        {
            return new List<Message>
            {
                new Message()
                {
                    MessageBody = "Hello mate, how are you?"  ,
                    Receiver = GetUserProfiles()[1],
                    Sender = GetUserProfiles()[0]

                },
                new Message()
                {
                    MessageBody = "Hi, I am fine?",
                    Receiver = GetUserProfiles()[0],
                    Sender = GetUserProfiles()[1]
                },
                new Message()
                {
                    MessageBody = "It is second dialog?",
                    Receiver = GetUserProfiles()[2],
                    Sender = GetUserProfiles()[1]
                },
                new Message()
                {
                    MessageBody = "Second?",
                    Receiver = GetUserProfiles()[1],
                    Sender = GetUserProfiles()[2]
                },
                new Message()
                {
                    MessageBody = "Third Dialog here?",
                    Receiver = GetUserProfiles()[3],
                    Sender = GetUserProfiles()[2]
                },
                new Message()
                {
                    MessageBody = "Third Dialog here2?",
                   Receiver = GetUserProfiles()[2],
                    Sender = GetUserProfiles()[3]
                },
                new Message()
                {
                    MessageBody = "Third Dialog here3?",
                    Receiver = GetUserProfiles()[3],
                    Sender = GetUserProfiles()[0]
                },
                new Message()
                {
                    MessageBody = "Fourth dialog",
                    Receiver = GetUserProfiles()[0],
                    Sender = GetUserProfiles()[3]
                }
            };
        }

        private static List<Dialog> GetDialogs()
        {
            return new List<Dialog>
            {
                new Dialog()
                {
                    Messages = new List<Message>
                    {
                        new Message{ MessageBody = "Hello mate, how are you?" },
                        new Message{ MessageBody = "Hi, I am fine?" }
                    }
                },
                new Dialog()
                {
                    Messages = new List<Message>
                    {
                        new Message{ MessageBody = "Hello mate, how are you? 2" },
                        new Message{ MessageBody = "Hi, I am fine? 2" }
                    }
                },
                new Dialog()
                {
                    Messages = new List<Message>
                    {
                        new Message{ MessageBody = "Hello mate, how are you? 3" },
                        new Message{ MessageBody = "Hi, I am fine? 3" }
                    }
                },
                new Dialog()
                {
                    Messages = new List<Message>
                    {
                        new Message{ MessageBody = "Hello mate, how are you? 4" },
                        new Message{ MessageBody = "Hi, I am fine? 4" }
                    }
                },
            };
        }
    }
}
