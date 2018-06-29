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
    public class ManageControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_Manage_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new ManageController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new ManageController().GetType(), TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Manage_Instantiate_Default_Application_User_Manager_And_Signin_Manager_Should_Pass()
        {
            // Arrage
            var controller = new ManageController((ApplicationUserManager)null, (ApplicationSignInManager)null);

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new ManageController().GetType(), TestContext.TestName);
        }
        #endregion Instantiate
    }
}
