using System;
using System.Collections.Generic;
using MarketplaceMVC.Data.Repositories;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service;
using System.Web.Mvc;
using MarketplaceMVC.Web.Areas.User.Controllers;
using MarketplaceMVC.Web.Areas.User.Models.Dialog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Security.Claims;
using System.Threading;

namespace MarketplaceMVC.UnitTests
{
    [TestClass]
    class UnitTest1
    {
        
        public void GetUnreadMessages()
        {
            Mock<IDialogService> mockDialog = new Mock<IDialogService>();

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("test");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            UserProfile user = new UserProfile()
            {
                Id = 0
            };

            mockDialog.Setup(m => m.GetAllDialogs()).Returns(new List<Dialog>
            {
                new Dialog()
                {
                    CompanionId = 0, CreatorId = 2,
                    Companion = user,
                    Messages = new List<Message>()
                            {
                                new Message(){ ToViewed = false, ReceiverId = 0, SenderId = 2}
                            }
                        },
                new Dialog()
                {
                    CompanionId = 0, CreatorId = 2,
                            Messages = new List<Message>()
                            {
                                new Message(){ ToViewed = false, ReceiverId = 0, SenderId = 2}
                            }
                        },
                new Dialog()
                {
                    CompanionId = 0, CreatorId = 2,
                    Messages = new List<Message>()
                    {
                        new Message(){ ToViewed = true, ReceiverId = 0, SenderId = 2}
                    }
                }
    }
            );
            DialogController controller = new DialogController(mockDialog.Object, null);
            controller.ControllerContext = controllerContext.Object;
            var q = controller.Unread().Result as ViewResult;
            var result = (DialogListViewModel)(q).Model;

            IEnumerable<DialogViewModel> dialogs = result.Dialogs;

            Assert.IsTrue(dialogs.Count() == 2);

        }
    }
}
