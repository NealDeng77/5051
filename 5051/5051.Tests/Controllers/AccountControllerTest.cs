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

        #region ResetPasswordConfirmationRegion

        [TestMethod]
        public void Controller_Account_ResetPasswordConfirmation_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ResetPasswordConfirmation() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ResetPasswordConfirmationRegion

        #region ExternalLoginFailureRegion

        [TestMethod]
        public void Controller_Account_ExternalLoginFailure_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ExternalLoginFailure() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ExternalLoginFailureRegion

        #region LoginRegion

        [TestMethod]
        public void Controller_Account_Login_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            string url = "abc";
            string expect = url;

            // Act
            var result = controller.Login(url) as ViewResult;

            var resultBag = controller.ViewBag;

            string resultURL = "";
            resultURL = controller.ViewData["ReturnUrl"].ToString();

            // Assert
            Assert.AreEqual(expect,resultURL, "URL " + TestContext.TestName);
            Assert.IsNotNull(result, "Null "+ TestContext.TestName);
        }

        #endregion LoginRegion

    }
}
