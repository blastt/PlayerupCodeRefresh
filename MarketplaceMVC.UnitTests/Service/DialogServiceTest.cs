using MarketplaceMVC.Data.Infrastructure;
using MarketplaceMVC.Data.Repositories;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service;
using MarketplaceMVC.Web.Areas.User.Controllers;
using MarketplaceMVC.Web.Automapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.UnitTests.Service
{
    [TestClass]
    public class DialogServiceTest
    {
        private IDialogService _dialogService;
        private Mock<IDialogRepository> _dialogRepositoryMock;
        DialogController objController;
        List<Dialog> listDialog;
        List<User> listUser;

        [TestInitialize]
        public void Initialize()
        {
            AutoMapperConfiguration.Configure();
            _dialogRepositoryMock = new Mock<IDialogRepository>();

            _dialogService = new DialogService(_dialogRepositoryMock.Object, null, null);
            listDialog = new List<Dialog>();
            listUser = new List<User>();
            var dialog1 = new Dialog()
            {
                Id = 1
            };
            var dialog2 = new Dialog()
            {
                Id = 2
            };
            var dialog3 = new Dialog()
            {
                Id = 3
            };
            var dialog4 = new Dialog()
            {
                Id = 4
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

            listDialog.AddRange(new List<Dialog> { dialog1, dialog2, dialog3, dialog4 });
            listUser.AddRange(new List<User> { user1, user2, user3, user4 });
        }
        [TestMethod]
        public void Dialog_GetPrivateDialog()
        {
            //Act
            var dialog = _dialogService.GetPrivateDialog(listUser[1].UserProfile, listUser[2].UserProfile);

            //Assert
            Assert.IsNotNull(dialog);
            Assert.AreEqual(dialog.Id, 2);
        }
        //GetPrivateDialog
    }
}

