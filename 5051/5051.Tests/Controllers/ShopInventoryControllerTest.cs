using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Controllers;
using _5051.Models;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class ShopInventoryControllerTest
    {
        public TestContext TestContext { get; set; }

        #region IndexRegion
        [TestMethod]
        public void Controller_ShopInventory_Index_Should_Pass()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            var resultShopInventoryViewModel = result.Model as ShopInventoryViewModel;

            // Assert
            Assert.IsNotNull(resultShopInventoryViewModel, TestContext.TestName);
        }
        #endregion IndexRegion
    }
}
