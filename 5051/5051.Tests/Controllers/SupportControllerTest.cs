using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Controllers;
using _5051.Backend;
using _5051.Models;
using Moq;

using System.Web;
//using Microsoft.AspNet.Identity.Owin;
using System.Security.Principal;

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

        #region UserListRegion

        [TestMethod]
        public void Controller_Support_UserList_Default_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();

            Backend.IdentityBackend.SetDataSource(DataSourceEnum.Mock);

            // Act
            var result = controller.UserList() as ViewResult;

            // Reset
            Backend.IdentityBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion UserListRegion

        #region UserInfoRegion

        [TestMethod]
        public void Controller_Support_UserInfo_Default_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();

            Backend.IdentityBackend.SetDataSource(DataSourceEnum.Mock);
            var supportUser = Backend.IdentityBackend.Instance.CreateNewSupportUser("user", "password", "id");

            string id = supportUser.Id;

            // Act
            var result = controller.UserInfo(id) as ViewResult;

            // Reset
            Backend.IdentityBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion UserInfoRegion

        #region ToggleUserRegion

        [TestMethod]
        public void Controller_Support_ToggleUser_Id_Null_Should_Return_Index_Page()
        {
            // Arrange
            var controller = new SupportController();

            string id = null;
            string item = "item";

            // Act
            var result = controller.ToggleUser(id, item) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_ToggleUser_Item_Null_Should_Return_Index_Page()
        {
            // Arrange
            var controller = new SupportController();

            string id = "id";
            string item = null;

            // Act
            var result = controller.ToggleUser(id, item) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_ToggleUser_UserNotFound_Should_Return_Index_Page()
        {
            // Arrange
            var controller = new SupportController();

            Backend.IdentityBackend.SetDataSource(DataSourceEnum.Mock);

            string id = "id";
            string item = "item";

            // Act
            var result = controller.ToggleUser(id, item) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion ToggleUserRegion

        #region ToggleUserPostRegion

        [TestMethod]
        public void Controller_Support_ToggleUser_Post_Model_Invalid_Should_Send_Back_For_Edit()
        {
            // Arrange
            var controller = new SupportController();
            var app = new ApplicationUserInputModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = controller.ToggleUser(app) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_ToggleUser_Post_data_Null_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new SupportController();
            var app = new ApplicationUserInputModel();
            app = null;

            // Act
            var result = controller.ToggleUser(app) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_ToggleUser_Post_id_Null_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new SupportController();
            var app = new ApplicationUserInputModel();
            app.Id = null;

            // Act
            var result = controller.ToggleUser(app) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_ToggleUser_Post_Role_Unknown_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new SupportController();
            var app = new ApplicationUserInputModel();
            app.Id = "id";
            app.Role = UserRoleEnum.Unknown;

            // Act
            var result = controller.ToggleUser(app) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_ToggleUser_Post_UserHasClaimValue_Should_Return_UserList_Page()
        {
            // Arrange
            var controller = new SupportController();
            var app = new ApplicationUserInputModel();

            // Todo: Troy, why is this not IdentityBackend?

            Backend.IdentityDataSourceTable.Instance.LoadDataSet(DataSourceDataSetEnum.Default);
            var supportUser = Backend.IdentityDataSourceTable.Instance.ListAllSupportUsers()[0];

            app.Id = supportUser.Id;
            app.Role = UserRoleEnum.SupportUser;

            // Act
            var result = controller.ToggleUser(app) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("UserList", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_ToggleUser_Post_AddClaimValue_Should_Return_UserList_Page()
        {
            // Arrange
            var controller = new SupportController();
            var app = new ApplicationUserInputModel();

            // Todo: Troy, why is this not IdentityBackend?

            Backend.IdentityDataSourceTable.Instance.LoadDataSet(DataSourceDataSetEnum.Default);
            var supportUser = Backend.IdentityDataSourceTable.Instance.ListAllSupportUsers()[0];

            app.Id = supportUser.Id;
            app.Role = UserRoleEnum.StudentUser;

            // Act
            var result = controller.ToggleUser(app) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("UserList", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion ToggleUserPostRegion

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
            loginViewModel.Email = DataSourceBackend.Instance.StudentBackend.GetDefault().Name;
            loginViewModel.Password = DataSourceBackend.Instance.StudentBackend.GetDefault().Password;

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

        [TestMethod]
        public void Controller_Support_CreateTeacher_Post_Default_Should_Return_UserList_Page()
        {
            // Arrange
            var controller = new SupportController();
            LoginViewModel loginViewModel = new LoginViewModel();

            // Todo: Troy, why is this not IdentityBackend?

            Backend.IdentityDataSourceTable.Instance.LoadDataSet(DataSourceDataSetEnum.Default);
            var teacherUser = Backend.IdentityDataSourceTable.Instance.ListAllTeacherUsers()[0];

            loginViewModel.Email = teacherUser.Email;
            loginViewModel.Password = teacherUser.PasswordHash;

            // Act
            var result = controller.CreateTeacher(loginViewModel) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("UserList", result.RouteValues["action"], TestContext.TestName);
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

        [TestMethod]
        public void Controller_Support_CreateSupport_Post_Default_Should_Return_UserList_Page()
        {
            // Arrange
            var controller = new SupportController();
            LoginViewModel loginViewModel = new LoginViewModel();

            // Todo: Troy, why is this not IdentityBackend?

            Backend.IdentityDataSourceTable.Instance.LoadDataSet(DataSourceDataSetEnum.Default);
            var supportUser = Backend.IdentityDataSourceTable.Instance.ListAllSupportUsers()[0];

            loginViewModel.Email = supportUser.Email;
            loginViewModel.Password = supportUser.PasswordHash;

            // Act
            var result = controller.CreateSupport(loginViewModel) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("UserList", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion CreateSupportPostRegion

        #region DeleteUserRegion
        [TestMethod]
        public void Controller_Support_Delete_Id_Null_Should_Return_UserList()
        {
            //arrange
            SupportController controller = new SupportController();
            string id = null;

            //act
            var result = controller.DeleteUser(id) as ActionResult;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_Delete_Default_Should_Pass()
        {
            //arrange
            SupportController controller = new SupportController();

            // Todo: Troy, why is this not IdentityBackend?

            Backend.IdentityDataSourceTable.Instance.LoadDataSet(DataSourceDataSetEnum.Default);
            var supportUser = Backend.IdentityDataSourceTable.Instance.ListAllSupportUsers()[0];

            string id = supportUser.Id;

            //act
            var result = controller.DeleteUser(id) as ActionResult;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion DeleteUserRegion

        #region DeleteUserPostRegion
        [TestMethod]
        public void Controller_Support_Delete_Post_Id_Null_Should_Pass()
        {
            //arrange
            SupportController controller = new SupportController();
            ApplicationUser app = new ApplicationUser();
            app.Id = null;

            //act
            var result = controller.DeleteUser(app) as ViewResult;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_Delete_Post_Default_Should_Return_UserList_Page()
        {
            //arrange
            SupportController controller = new SupportController();
            ApplicationUser app = new ApplicationUser();

            // Todo: Troy, why is this not IdentityBackend?

            Backend.IdentityDataSourceTable.Instance.LoadDataSet(DataSourceDataSetEnum.Default);
            var supportUser = Backend.IdentityDataSourceTable.Instance.ListAllSupportUsers()[0];

            app.Id = supportUser.Id;

            //act
            var result = controller.DeleteUser(app) as RedirectToRouteResult;

            //assert
            Assert.AreEqual("UserList", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion DeleteUserPostRegion

        #region SettingsRegion

        [TestMethod]
        public void Controller_Support_Settings_Default_Should_Pass()
        {
            // Arrange
            var controller = new SupportController();

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

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
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

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
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

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
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

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
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSourceSet(myId);

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

        }

        [TestMethod]
        public void Controller_Support_DataSourceSet_Id_Equals_Demo_Should_Return_Index_Page()
        {
            // Arrange
            var controller = new SupportController();
            var myId = "Demo";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSourceSet(myId);

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            //Check that the DataSourceSet is set to Demo using avatar count, Demo set not implemented yet, set to Default
        }

        [TestMethod]
        public void Controller_Support_DataSourceSet_Id_Equals_UnitTest_Should_Return_Index_Page()
        {
            // Arrange
            var controller = new SupportController();
            var myId = "UnitTest";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSourceSet(myId);

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            //Check that the DataSourceSet is set to UnitTest using avatar count, UnitTest set not implemented yet, set to Default
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
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

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
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

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
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            //Check that the DataSource is set to Mock using avatar count
        }

        [TestMethod]
        public void Controller_Support_DataSource_Id_Equals_SQL_Should_Return_Index_Page()
        {
            // Arrange
            SupportController controller = new SupportController();
            var myId = "SQL";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_DataSource_Id_Equals_Local_Should_Return_Index_Page()
        {
            // Arrange
            SupportController controller = new SupportController();
            var myId = "Local";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_DataSource_Id_Equals_ServerTest_Should_Return_Index_Page()
        {
            // Arrange
            SupportController controller = new SupportController();
            var myId = "ServerTest";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_DataSource_Id_Equals_ServerLive_Should_Return_Index_Page()
        {
            // Arrange
            SupportController controller = new SupportController();
            var myId = "ServerLive";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_DataSource_Id_Equals_Unknown_Should_Return_Index_Page()
        {
            // Arrange
            SupportController controller = new SupportController();
            var myId = "unknown";

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(p => p.Identity.Name).Returns("name");
            userMock.Setup(p => p.IsInRole(_5051.Models.UserRoleEnum.SupportUser.ToString())).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            contextMock.SetupGet(p => p.Request.IsAuthenticated).Returns(true);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            var result = (RedirectToRouteResult)controller.DataSource(myId);

            //Reset the data source backend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            //Check that the DataSource is set to unknown using avatar count, unknown DataSource not implemented yet, set to Mock
        }

        #endregion DataSourceRegion

        #region ChangeUserPassword
        [TestMethod]
        public void Controller_Support_ChangeUserPassword_Id_Null_Should_Return_UserList_Page()
        {
            //arrange
            SupportController controller = new SupportController();
            string expect = null;

            //act
            var result = controller.ChangeUserPassword(expect) as ActionResult;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_ChangeUserPassword_Default_Should_Pass()
        {
            //arrange
            SupportController controller = new SupportController();

            Backend.IdentityBackend.SetDataSource(DataSourceEnum.Mock);
            var supportUser = Backend.IdentityBackend.Instance.CreateNewSupportUser("user", "password", "id");

            string expect = supportUser.Id;

            //act
            var result = controller.ChangeUserPassword(expect) as ViewResult;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion

        #region ChangeUserPasswordPostRegion
        [TestMethod]
        public void Controller_Support_ChangeUserPassword_Post_Id_Null_Should_Pass()
        {
            //arrange
            var dataBackend = DataSourceBackend.Instance;
            SupportController controller = new SupportController();
            ChangePasswordViewModel viewModel = new ChangePasswordViewModel();
            viewModel.UserID = null;

            //act
            var result = controller.ChangeUserPassword(viewModel) as ViewResult;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_ChangeUserPassword_Post_StudentUser_Should_Pass()
        {
            //arrange
            IdentityBackend.Instance.Reset();
            SupportController controller = new SupportController();
            ChangePasswordViewModel viewModel = new ChangePasswordViewModel();
            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();
            viewModel.UserID = student.Id;
            viewModel.NewPassword = student.Password;
            viewModel.ConfirmPassword = student.Password;
            viewModel.OldPassword = student.Password;

            //act
            var result = controller.ChangeUserPassword(viewModel) as RedirectToRouteResult;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_ChangeUserPassword_Post_isSupportUser_Should_Return_UserList_Page()
        {
            //arrange
            SupportController controller = new SupportController();
            ChangePasswordViewModel viewModel = new ChangePasswordViewModel();

            Backend.IdentityBackend.SetDataSource(DataSourceEnum.Mock);
            var supportUser = Backend.IdentityBackend.Instance.ListAllSupportUsers()[0];
            viewModel.UserID = supportUser.Id;

            //act
            var result = controller.ChangeUserPassword(viewModel) as RedirectToRouteResult;

            //assert
            Assert.AreEqual("UserList", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Support_ChangeUserPassword_Post_isTeacherUser_Should_Return_UserList_Page()
        {
            //arrange
            SupportController controller = new SupportController();
            ChangePasswordViewModel viewModel = new ChangePasswordViewModel();

            Backend.IdentityBackend.SetDataSource(DataSourceEnum.Mock);

            viewModel.UserID = "teacher";

            //act
            var result = controller.ChangeUserPassword(viewModel) as RedirectToRouteResult;

            //assert
            Assert.AreEqual("UserList", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion ChangeUserPasswordPostRegion

        [TestMethod]
        public void Controller_Support_Reset_DeFault_Should_Return_Index_Page()
        {
            // Arrange
            SupportController controller = new SupportController();

            // Act
            var result = (RedirectToRouteResult)controller.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }
    }
}
