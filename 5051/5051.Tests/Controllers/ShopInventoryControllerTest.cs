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

        #region Instantiate
        [TestMethod]
        public void Controller_ShopInventory_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new ShopInventoryController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new ShopInventoryController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

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

            // Assert
            Assert.AreEqual(null, result.Model, TestContext.TestName);
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

        #region CreateRegion
        [TestMethod]
        public void Controller_ShopInventory_Create_Get_Should_Return_New_Model()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();

            // Act
            ViewResult result = controller.Create() as ViewResult;
            var resultShopInventoryModel = result.Model as ShopInventoryModel;

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.AreNotEqual(null, resultShopInventoryModel.Id, TestContext.TestName);
        }
        #endregion CreateRegion

        #region CreatePostRegion
        [TestMethod]
        public void Controller_ShopInventory_Create_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Create(data) as ViewResult;

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ShopInventory_Create_Post_Invalid_Should_Return_ErrorPage()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();
            data = null;

            // Act
            var result = (RedirectToRouteResult)controller. Create(data);

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ShopInventory_Create_Post_Invalid_IDIsNull_Should_Return_Model()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();          
            data.Id = null;

            // Act
            ViewResult result = controller.Create(data) as ViewResult;
            var resultShopInventoryModel = result.Model as ShopInventoryModel;

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(data.Description, resultShopInventoryModel.Description, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ShopInventory_Create_Post_Valid_Should_Return_IndexPage()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();         

            // Act
            var result = (RedirectToRouteResult)controller.Create(data);
            var resultShopInventoryModel = ShopInventoryBackend.Instance.Create(data);

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(data.Description, resultShopInventoryModel.Description, TestContext.TestName);
        }
        #endregion CreatePostRegion

        #region UpdateRegion
        [TestMethod]
        public void Controller_ShopInventory_Update_IDIsNull_Should_Pass()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();

            // Act
            ViewResult result = controller.Update() as ViewResult;

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(null, result.Model, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ShopInventory_Update_IDValid_Should_Pass()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();
            string id = Backend.ShopInventoryBackend.Instance.Create(data).Id;

            // Act
            ViewResult result = controller.Update(id) as ViewResult;

            var resultShopInventoryModel = result.Model as ShopInventoryModel;

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(resultShopInventoryModel, TestContext.TestName);
        }
        #endregion UpdateRegion

        #region UpdatePostRegion
        [TestMethod]
        public void Controller_ShopInventory_Update_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Update(data) as ViewResult;

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ShopInventory_Update_Post_Invalid_Should_Return_ErrorPage()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();
            data = null;

            // Act
            var result = (RedirectToRouteResult)controller.Update(data);

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ShopInventory_Update_Post_Invalid_IDIsNull_Should_Return_Model()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();
            data.Id = null;

            // Act
            ViewResult result = controller.Update(data) as ViewResult;
            var resultShopInventoryModel = result.Model as ShopInventoryModel;

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(data.Description, resultShopInventoryModel.Description, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ShopInventory_Update_Post_Valid_Should_Return_IndexPage()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();

            // Act
            var result = (RedirectToRouteResult)controller.Update(data);
            var resultShopInventoryModel = ShopInventoryBackend.Instance.Create(data);

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(data.Description, resultShopInventoryModel.Description, TestContext.TestName);
        }
        #endregion UpdatePostRegion

        #region DeleteRegion
        [TestMethod]
        public void Controller_ShopInventory_Delete_IDIsNull_Should_Pass()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();

            // Act
            ViewResult result = controller.Delete() as ViewResult;

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(null, result.Model, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ShopInventory_Delete_IDValid_Should_Pass()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();
            string id = Backend.ShopInventoryBackend.Instance.Create(data).Id;

            // Act
            ViewResult result = controller.Delete(id) as ViewResult;
            var resultShopInventoryModel = result.Model as ShopInventoryModel;

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(resultShopInventoryModel, TestContext.TestName);
        }
        #endregion DeleteRegion

        #region DeletePostRegion
        [TestMethod]
        public void Controller_ShopInventory_Delete_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Delete(data) as ViewResult;

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ShopInventory_Delete_Post_Invalid_Should_Return_ErrorPage()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();
            data = null;

            // Act
            var result = (RedirectToRouteResult)controller.Delete(data);

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ShopInventory_Delete_Post_Invalid_IDIsNull_Should_Return_Model()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();
            data.Id = null;

            // Act
            ViewResult result = controller.Delete(data) as ViewResult;
            var resultShopInventoryModel = result.Model as ShopInventoryModel;

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(data.Description, resultShopInventoryModel.Description, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ShopInventory_Delete_Post_Valid_Should_Return_IndexPage()
        {
            // Arrange
            ShopInventoryController controller = new ShopInventoryController();
            ShopInventoryModel data = new ShopInventoryModel();

            // Act
            var result = (RedirectToRouteResult)controller.Delete(data);
            var resultShopInventoryModel = ShopInventoryBackend.Instance.Create(data);

            // Reset ShopInventoryBackend
            ShopInventoryBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(data.Description, resultShopInventoryModel.Description, TestContext.TestName);
        }
        #endregion DeletePostRegion
    }
}
