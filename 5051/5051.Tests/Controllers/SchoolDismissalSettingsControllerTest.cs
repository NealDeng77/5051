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
    public class SchoolDismissalSettingsControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_SchoolDismissalSettings_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new AdminController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new AdminController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region ReadRegion
        [TestMethod]
        public void Controller_SchoolDismissalSetting_Read_Null_Id_Should_Return_Default_Model()
        {
            // Arrange
            var controller = new SchoolDismissalSettingsController();
            string id = null;

            // Act
            var result = controller.Read(id);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_SchoolDismissalSetting_Read_No_Id_Should_Return_Default_Model()
        {
            // Arrange
            var controller = new SchoolDismissalSettingsController();

            // Act
            var result = controller.Read();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_SchoolDismissalSetting_Read_Invalid_Id_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new SchoolDismissalSettingsController();
            string id = "bogus";

            // Act
            var result = (RedirectToRouteResult)controller.Read(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }
        #endregion ReadRegion
    }
}
