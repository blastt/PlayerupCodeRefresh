using MarketplaceMVC.Data.Infrastructure;
using MarketplaceMVC.Data.Repositories;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Web.Automapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.UnitTests.Repository
{
    [TestClass]
    class GameRepositoryTest
    {
        DbConnection connection;
        TestContext databaseContext;
        GameRepository objRepo;

        [TestInitialize]
        public void Initialize()
        {
           

        }

        [TestMethod]
        public void Country_GetGameByValue()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Get(g => g.Value == "dota2")).Returns(new Game { Id = 2, Value = "dota2" });
            //Act
            var result = objRepo.GetGameByValue("dota2");

            //Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Id);
        }
    }
}
