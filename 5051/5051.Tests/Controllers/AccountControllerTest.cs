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
    public class AccountControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_Account_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new AccountController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region AccountUserSignInManagersRegion

        [TestMethod]
        public void Controller_Account_User_SignIn_Managers_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController((ApplicationUserManager)null, (ApplicationSignInManager)null);

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new AccountController().GetType(), TestContext.TestName);
        }

        #endregion AccountUserSignInManagersRegion

        #region RegisterRegion

        [TestMethod]
        public void Controller_Account_Register_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.Register() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion RegisterRegion

        #region ForgotPasswordRegion

        [TestMethod]
        public void Controller_Account_ForgotPassword_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ForgotPassword() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ForgotPasswordRegion

        #region ForgotPasswordConfirmationRegion

        [TestMethod]
        public void Controller_Account_ForgotPasswordConfirmation_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ForgotPasswordConfirmation() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ForgotPasswordConfirmationRegion
    }
}
