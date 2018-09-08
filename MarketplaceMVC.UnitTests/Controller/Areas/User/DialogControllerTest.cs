using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service;
using MarketplaceMVC.Web.Areas.User.Controllers;
using MarketplaceMVC.Web.Areas.User.Models.Dialog;
using MarketplaceMVC.Web.Automapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MarketplaceMVC.UnitTests.Controller.Areas
{
    [TestClass]
    public class DialogControllerTest
    {
        private Mock<IDialogService> _dialogServiceMock;
        DialogController objController;
        List<Dialog> listCountry;

        [TestInitialize]
        public void Initialize()
        {
            AutoMapperConfiguration.Configure();
            _dialogServiceMock = new Mock<IDialogService>();
            objController = new DialogController(_dialogServiceMock.Object, null);
            listCountry = new List<Dialog>();
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

            listCountry.AddRange(new List<Dialog> { dialog1, dialog2, dialog3, dialog4 });
        }
        [TestMethod]
        public void Dialog_Get_Unread_Messages()
        {
            Dialog d1 = new Dialog { Id = 1 };
            Dialog d2 = new Dialog { Id = 2 };

            var context = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            context.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns("test_name");

            Mock<IDialogService> mock = new Mock<IDialogService>();
            mock.Setup(m => m.GetUserDialogsAsync(0, d => d.Messages.Any(t => t.ToViewed))).ReturnsAsync(new List<Dialog>()
            {
                new Dialog() { Id = 1 },
                new Dialog() { Id = 2 },
                new Dialog() { Id = 3 }
            });
            DialogController controller = new DialogController(mock.Object, null);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            //Arrange


            //Act
            var result = ((controller.Unread().Result).Model) as IEnumerable<DialogViewModel>;

            //Assert
            Assert.AreEqual(result.Count(), 3);
        }

        [TestMethod]
        public void Dialog_Details()
        {
            Dialog d1 = new Dialog { Id = 1 };
            Dialog d2 = new Dialog { Id = 2 };

            Mock<IDialogService> mock = new Mock<IDialogService>();
            mock.Setup(m => m.GetDialogAsync(2)).ReturnsAsync(new Dialog { Id = 2 });
            DialogController controller = new DialogController(mock.Object, null);
            //Arrange


            //Act
            var result = ((controller.Details(2).Result).Model) as DetailsDialogViewModel;

            //Assert
            Assert.AreEqual(result.Id, 2);
        }

    }
}
