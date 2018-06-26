using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Controllers;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class ShopInventoryControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_ShopInventory_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new AdminController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new AdminController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate
    }
}
