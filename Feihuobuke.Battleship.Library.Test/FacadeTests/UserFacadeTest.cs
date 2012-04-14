using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feihuobuke.Battleship.Library.Test.FacadeTests
{
    [TestClass]
    public class UserFacadeTest
    {
        private UserDao Intance { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Intance = new UserDao()
                          {
                              Name = "Test" + DateTime.Now.Ticks,
                              Password = "Test" + DateTime.Now.Ticks,
                              Active = true
                          };
        }

        [TestMethod]
        public void CreateTest()
        {
            var facade = UserFacade.GetInstance();
            facade.Insert(Intance);

            Assert.IsTrue(Intance.Id > 0);
        }
    }
}