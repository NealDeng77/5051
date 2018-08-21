using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Controllers;
using _5051.Backend;
using _5051.Models;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class KioskSettingsControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_KioskSettings_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new KioskSettingsController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new KioskSettingsController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region ReadRegion
        [TestMethod]
        public void Controller_KioskSettings_Read_Id_Is_Null_Should_Return_Default_Model()
        {
            // Arrange
            var controller = new KioskSettingsController();

            string id = null;

            // Act
            var result = controller.Read(id);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_KioskSettings_Read_No_Id_Should_Return_Default_Model()
        {
            // Arrange
            var controller = new KioskSettingsController();

            // Act
            var result = controller.Read();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_KioskSettings_Read_Invalid_Id_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new KioskSettingsController();
            string id = "bogus";

            // Act
            var result = (RedirectToRouteResult)controller.Read(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }
        #endregion ReadRegion

        #region UpdateGetRegion
        [TestMethod]
        public void Controller_KioskSettings_Update_Get_Default_Should_Pass()
        {
            // Arrange
            var controller = new KioskSettingsController();

            string id = DataSourceBackend.Instance.KioskSettingsBackend.GetDefault().Id;

            // Act
            var result = (ViewResult)controller.Update(id);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_KioskSettings_Update_Get_Invalid_Id_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new KioskSettingsController();

            string id = "bogus";

            // Act
            var result = (RedirectToRouteResult)controller.Update(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }
        #endregion UpdateGetRegion

        #region PostUpdateRegion
        [TestMethod]
        public void Controller_KioskSettings_Post_Update_Invalid_Model_Should_Send_Back_For_Edit()
        {
            // Arrange
            var controller = new KioskSettingsController();
            KioskSettingsModel data = new KioskSettingsModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = (ViewResult)controller.Update(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_KioskSettings_Post_Update_Null_Data_Should_Return_Error()
        {
            // Arrange
            var controller = new KioskSettingsController();

            // Act
            var result = (RedirectToRouteResult)controller.Update((KioskSettingsModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_KioskSettings_Post_Update_Null_Id_Should_Send_Back_For_Edit()
        {
            // Arrange
            var controller = new KioskSettingsController();
            KioskSettingsModel dataNull = new KioskSettingsModel();

            // Make data.Id = null
            dataNull.Id = null;

            // Act
            var resultNull = (ViewResult)controller.Update(dataNull);

            // Assert
            Assert.IsNotNull(resultNull, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_KioskSettings_Post_Update_Empty_Id_Should_Send_Back_For_Edit()
        {
            // Arrange
            var controller = new KioskSettingsController();
            KioskSettingsModel dataEmpty = new KioskSettingsModel();

            // Make data.Id empty
            dataEmpty.Id = "";

            // Act
            var resultEmpty = (ViewResult)controller.Update(dataEmpty);

            // Assert
            Assert.IsNotNull(resultEmpty, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_KioskSettings_Post_Update_Default_Should_Return_Admin_Settings_Page()
        {
            // Arrange
            var controller = new KioskSettingsController();

            // Get default KioskSettingsModel
            KioskSettingsModel kioskSettingsModel = DataSourceBackend.Instance.KioskSettingsBackend.GetDefault();

            // Act
            var result = (RedirectToRouteResult)controller.Update(kioskSettingsModel);

            // Assert
            Assert.AreEqual("Settings", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Admin", result.RouteValues["controller"], TestContext.TestName);
        }
        #endregion PostUpdateRegion
    }
}
