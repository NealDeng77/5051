﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Controllers;
using _5051.Backend;
using _5051.Models;
using Moq;

using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
using System.Security.Principal;
using System.Web.SessionState;
using System.IO;
using System.Reflection;

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
        public void Controller_Support_CreateStudent_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();
            LoginViewModel loginViewModel = new LoginViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = controller.CreateStudent(loginViewModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }


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

        #region CreateTeacherPostRegion

        [TestMethod]
        public void Controller_Support_CreateTeacher_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();
            LoginViewModel loginViewModel = new LoginViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = controller.CreateTeacher(loginViewModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion CreateTeacherPostRegion

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

        #region SettingsRegion

        [TestMethod]
        public void Controller_Support_Settings_Default_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = controller.Settings() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion SettingsRegion

        #region ResetRegion

        [TestMethod]
        public void Controller_Support_Reset_Default_Should_Return_Index_Page()
        {
            // Arrange
            var controller = new SupportController();

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = controller.Reset() as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion ResetRegion

        #region DataSourceSetRegion 

        [TestMethod]
        //[ExpectedException(typeof(System.ArgumentNullException))]
        public void Controller_Support_DataSourceSet_Id_Is_Null_Should_Return_Index_Page()
        {
            // Arrange

            var controller = new SupportController();

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var resultPage = (RedirectToRouteResult)controller.DataSourceSet(null);

            // Assert
            Assert.AreEqual("Index", resultPage.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_DataSourceSet_Id_Is_Empty_Should_Return_Index_Page()
        {
            // Arrange
            var controller = new SupportController();

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSourceSet("");

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_DataSourceSet_Id_Equals_DeFault_Should_Return_Index_Page()
        {
            // Arrange
            var controller = new SupportController();
            var myId = "Default";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSourceSet(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            //Check that the DataSourceSet is set to Default using avatar count
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_DataSourceSet_Id_Equals_Demo_Should_Return_Index_Page()
        {
            // Arrange
            var controller = new SupportController();
            var myId = "Demo";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSourceSet(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            //Check that the DataSourceSet is set to Demo using avatar count, Demo set not implemented yet, set to Default
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);

        }

        [TestMethod]
        public void Controller_Support_DataSourceSet_Id_Equals_UnitTest_Should_Return_Index_Page()
        {
            // Arrange
            var controller = new SupportController();
            var myId = "UnitTest";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSourceSet(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            //Check that the DataSourceSet is set to UnitTest using avatar count, UnitTest set not implemented yet, set to Default
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);
        }

        #endregion DataSourceSetRegion

        #region DataSourceRegion

        [TestMethod]
        public void Controller_Support_DataSource_Id_Is_Null_Should_Return_Index_Page()
        {
            // Arrange
            SupportController controller = new SupportController();

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(null);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_DataSource_Id_Is_Empty_Should_Return_Index_Page()
        {
            // Arrange
            SupportController controller = new SupportController();

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSource("");

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_DataSource_Id_Equals_Mock_Should_Return_Index_Page()
        {
            // Arrange
            SupportController controller = new SupportController();
            var myId = "Mock";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            //Check that the DataSource is set to Mock using avatar count
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);

        }

        [TestMethod]
        public void Controller_Support_DataSource_Id_Equals_SQL_Should_Return_Index_Page()
        {
            // Arrange
            SupportController controller = new SupportController();
            var myId = "SQL";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;


            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
            //Check that the DataSource is set to SQL using avatar count, SQL DataSource not implemented yet, set to Mock
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_DataSource_Id_Equals_Local_Should_Return_Index_Page()
        {
            // Arrange
            SupportController controller = new SupportController();
            var myId = "Local";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;


            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
            //Check that the DataSource is set to SQL using avatar count, SQL DataSource not implemented yet, set to Mock
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_DataSource_Id_Equals_ServerTest_Should_Return_Index_Page()
        {
            // Arrange
            SupportController controller = new SupportController();
            var myId = "ServerTest";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;


            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
            //Check that the DataSource is set to SQL using avatar count, SQL DataSource not implemented yet, set to Mock
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_DataSource_Id_Equals_ServerLive_Should_Return_Index_Page()
        {
            // Arrange
            SupportController controller = new SupportController();
            var myId = "ServerLive";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;


            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
            //Check that the DataSource is set to SQL using avatar count, SQL DataSource not implemented yet, set to Mock
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_DataSource_Id_Equals_Unknown_Should_Return_Index_Page()
        {
            // Arrange
            SupportController controller = new SupportController();
            var myId = "unknown";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole("SupportUser")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            var resultAvatarCount = AvatarBackend.Instance.Index().Count;


            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            //Check that the DataSource is set to unknown using avatar count, unknown DataSource not implemented yet, set to Mock
            Assert.AreEqual(resultAvatarCount, 10, TestContext.TestName);
        }

        #endregion DataSourceRegion

    }
}
