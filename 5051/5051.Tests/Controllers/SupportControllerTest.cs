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
    public class SupportControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Controller_Support_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new SupportController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region SupportSignInManagersRegion

        [TestMethod]
        public void Controller_Support_User_SignIn_Managers_Default_Should_Pass()
        {
            // Arrange
            var controller = new SupportController((ApplicationUserManager)null, (ApplicationSignInManager)null);

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new SupportController().GetType(), TestContext.TestName);
        }

        #endregion SupportSignInManagersRegion

        #region IndexRegion
        [TestMethod]
        public void Controller_Support_Index_Default_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion IndexRegion

        #region LoginRegion
        [TestMethod]
        public void Controller_Support_Login_Default_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();

            // Act
            var result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion LoginRegion

        #region LoginPostRegion

        [TestMethod]
        public void Controller_Support_Login_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();
            LoginViewModel loginViewModel = new LoginViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = controller.Login(loginViewModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_Login_Post_loginResult_False_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();
            LoginViewModel loginViewModel = new LoginViewModel();

            // Act
            var result = controller.Login(loginViewModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        //[TestMethod]
        //public void Controller_Support_Login_Post_loginResult_True_Should_RedirectTo_Index()
        //{
        //    // Arrange
        //    var controller = new SupportController();
        //    LoginViewModel loginViewModel = new LoginViewModel();

        //    loginViewModel.Email = "name@seattleu.edu";
        //    loginViewModel.Password = "password";

        //    controller.CreateSupport(loginViewModel);

        //    // Act
        //    var result = controller.Login(loginViewModel) as RedirectToRouteResult;

        //    // Assert
        //    Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        //}


        #endregion LoginPostRegion

        #region CreateStudentRegion
        [TestMethod]
        public void Controller_Support_CreateStudent_Default_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();

            // Act
            var result = controller.CreateStudent() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion CreateStudentRegion

        #region CreateStudentPostRegion

        [TestMethod]
        public void Controller_Support_CreateStudent_Post_Default_Should_RedirectTo_UserList()
        {
            // Arrange
            var controller = new SupportController();
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.Email = StudentBackend.Instance.GetDefault().Name;
            loginViewModel.Password = StudentBackend.Instance.GetDefault().Password;

            // Act
            var result = controller.CreateStudent(loginViewModel) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("UserList", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion CreateStudentPostRegion

        #region CreateTeacherRegion
        [TestMethod]
        public void Controller_Support_CreateTeacher_Default_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();

            // Act
            var result = controller.CreateTeacher() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion CreateTeacherRegion

        #region CreateSupportRegion
        [TestMethod]
        public void Controller_Support_CreateSupport_Default_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();

            // Act
            var result = controller.CreateSupport() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion CreateSupportRegion

        #region CreateSupportPostRegion

        [TestMethod]
        public void Controller_Support_CreateSupport_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();
            LoginViewModel loginViewModel = new LoginViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = controller.CreateSupport(loginViewModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        //[TestMethod]
        //public void Controller_Support_CreateSupport_Post_Default_Should_Pass()
        //{
        //    // Arrange
        //    var controller = new SupportController();
        //    LoginViewModel loginViewModel = new LoginViewModel();

        //    // Act
        //    var result = controller.CreateSupport(loginViewModel) as RedirectToRouteResult;

        //    // Assert
        //    Assert.AreEqual("UserList", result.RouteValues["action"], TestContext.TestName);
        //}

        #endregion CreateSupportPostRegion

    }
}
