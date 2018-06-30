using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Controllers;
using _5051.Models;
using _5051.Backend;

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

        #region ReadRegion
        [TestMethod]
        public void Controller_ShopInventory_Read_IDIsNull_Should_Pass()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            string id = null;

            // Act
            ViewResult result = controller.Read(id) as ViewResult;

            var resultShopInventoryModel = result.Model as ShopInventoryModel;

            // Assert
            Assert.IsNotNull(resultShopInventoryModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ShopInventory_Read_IDValid_Should_Pass()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();
            string id = Backend.ShopInventoryBackend.Instance.Create(data).Id;

            // Act
            ViewResult result = controller.Read(id) as ViewResult;

            var resultShopInventoryModel = result.Model as ShopInventoryModel;

            // Reset StudentBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(resultShopInventoryModel, TestContext.TestName);
        }
        #endregion ReadRegion
    }
}
