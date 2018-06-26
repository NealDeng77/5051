using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Controllers;
using _5051.Models;
using System.Diagnostics;
using _5051.Backend;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class PortalControllerTest
    {
        public TestContext TestContext { get; set; }

        #region RosterRegion
        [TestMethod]
        public void Controller_Portal_Roster_Should_Return_NewModel()
        {
            // Arrange
            PortalController controller = new PortalController();

            // Act
            ViewResult result = controller.Roster() as ViewResult;

            var resultStudentViewModel = result.Model as StudentViewModel;

            // Assert
            Assert.IsNotNull(resultStudentViewModel, TestContext.TestName);
        }
        #endregion

        #region LoginStringRegion
        [TestMethod]
        public void Controller_Portal_Login_IDIsNull_Should_Return_RosterPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.Login(id);

            // Assert
            Assert.AreEqual("Roster", result.RouteValues["action"], TestContext.TestName);
        }

       [TestMethod]
        public void Controller_Portal_Login_IDValid_ShouldPass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = new StudentModel();
            string id = Backend.StudentBackend.Instance.Create(data).Id;

            // Act
            ViewResult result = controller.Login(id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }
        #endregion

        #region LoginPostRegion
        [TestMethod]
        public void Controller_Portal_Login_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();

            StudentDisplayViewModel data = new StudentDisplayViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Login(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Login_Post_Invalid_Should_Return_ErrorPage()
        {
            // Arrange
            PortalController controller = new PortalController();

            var data = new StudentDisplayViewModel();
            data = null;
            
            // Act
            var result = (RedirectToRouteResult)controller.Login(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);    
        }

        [TestMethod]
        public void Controller_Portal_Login_Post_Invalid_IDIsNull_Should_Return_Model()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel student = new StudentModel();
            var data = new StudentDisplayViewModel(student);
            data.Id = null;

            // Act
            ViewResult result = controller.Login(data) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Assert
            Assert.AreEqual(data.AvatarDescription, resultStudentDisplayViewModel.AvatarDescription, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Login_Post_Invalid_StudentModelIsNull_Should_Return_ErrorPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel student = new StudentModel();
            var data = new StudentDisplayViewModel(student);

            // Act
            var result = (RedirectToRouteResult)controller.Login(data);        

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Login_Post_Valid_Should_Return_IndexPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel student = new StudentModel("Peter", null);
            student.Id = Backend.StudentBackend.Instance.Create(student).Id;
            var data = new StudentDisplayViewModel(student);

            // Act
            var result = (RedirectToRouteResult)controller.Login(data);

            var resultStudent = StudentBackend.Instance.Read(data.Id);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(data.Id, resultStudent.Id, TestContext.TestName);
        }
        #endregion

        #region IndexStringRegion
        [TestMethod]
        public void Controller_Portal_Index_IDIsNull_Should_Return_RosterPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.Index(id);

            // Assert
            Assert.AreEqual("Roster", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Index_IDValid_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = new StudentModel();
            string id = Backend.StudentBackend.Instance.Create(data).Id;

            // Act
            ViewResult result = controller.Index(id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }
        #endregion

        #region AttendanceStringRegion
        [TestMethod]
        public void Controller_Portal_Attendance_IDIsNull_Should_Return_RosterPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.Attendance(id);

            // Assert
            Assert.AreEqual("Roster", result.RouteValues["action"], TestContext.TestName);
        }

       [TestMethod]
        public void Controller_Portal_Attendance_IDValid_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = new StudentModel();
            string id = Backend.StudentBackend.Instance.Create(data).Id;

            // Act
            ViewResult result = controller.Attendance(id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }
        #endregion

        #region AvatarPostRegion
        [TestMethod]
        public void Controller_Portal_Avatar_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();

            StudentAvatarModel data = new StudentAvatarModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Avatar(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Avatar_Post_Invalid_AvatarIDIsNull_Should_Return_ErrorPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            var data = new StudentAvatarModel();
            data.AvatarId = null;

            // Act
            var result = (RedirectToRouteResult)controller.Avatar(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Avatar_Post_Invalid_StudentIDIsNull_Should_Return_ErrorPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            var avartar = new AvatarModel();
            avartar.Id = Backend.AvatarBackend.Instance.Create(avartar).Id;
            var data = new StudentAvatarModel();
            data.AvatarId = avartar.Id;
            data.StudentId = null;

            // Act
            var result = (RedirectToRouteResult)controller.Avatar(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Avatar_Post_Invalid_StudentModelIsNull_Should_Return_ErrorPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            var avartar = new AvatarModel();
            avartar.Id = Backend.AvatarBackend.Instance.Create(avartar).Id;
            StudentModel student = new StudentModel();
            var data = new StudentAvatarModel();
            data.AvatarId = avartar.Id;
            data.StudentId = student.Id;
            

            // Act
            var result = (RedirectToRouteResult)controller.Avatar(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Avatar_Post_Valid_Should_Return_IndexPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            AvatarModel avartar = new AvatarModel();
            avartar.Id = Backend.AvatarBackend.Instance.Create(avartar).Id;
            StudentModel student = new StudentModel();
            student.Id = Backend.StudentBackend.Instance.Create(student).Id;
            var data = new StudentAvatarModel();
            data.AvatarId = avartar.Id;
            data.StudentId = student.Id;

            // Act
            var result = (RedirectToRouteResult)controller.Avatar(data);

            var resultAvatar = StudentBackend.Instance.Read(data.AvatarId);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);           
        }
        #endregion

        #region AvatarIDNullRegion
        #endregion

        #region GroupRegion
        [TestMethod]
        public void Controller_Portal_Group_Default_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();

            // Act
            ViewResult result = controller.Group() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion

        #region HouseRegion
        [TestMethod]
        public void Controller_Portal_House_IDIsNull_ShouldReturnRosterPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.House(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }
        
        [TestMethod]
        public void Controller_Portal_House_IDValid_ShouldPass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = new StudentModel();
            string id = Backend.StudentBackend.Instance.Create(data).Id;

            // Act
            ViewResult result = controller.House(id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }
        #endregion

        #region SettingsIDNullRegion
        [TestMethod]
        public void Controller_Portal_Settings_IDIsNull_ShouldReturnRosterPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.Settings(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }
        
        [TestMethod]
        public void Controller_Portal_Settings_IDValid_ShouldPass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = new StudentModel();
            string id = Backend.StudentBackend.Instance.Create(data).Id;

            // Act
            ViewResult result = controller.Settings(id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }
        #endregion

        #region SettingsBindRegion
        #endregion

        #region ReportRegion
        [TestMethod]
        public void Controller_Portal_Report_IDIsNull_ShouldReturnRosterPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.Report(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }           

        [TestMethod]
        public void Controller_Portal_Report_IDValid_shouldReturnErrorPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = new StudentModel();
            string id = Backend.StudentBackend.Instance.Create(data).Id;

            // Act
            var result = controller.Report(id) as ViewResult;

            var resultStudentReportViewModel = result.Model as StudentReportViewModel;

            // Assert
            Assert.IsNotNull(resultStudentReportViewModel, TestContext.TestName);
        }
        #endregion
    }
}
