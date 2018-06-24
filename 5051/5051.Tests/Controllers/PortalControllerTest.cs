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

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class PortalControllerTest
    {
        public TestContext TestContext { get; set; }

        #region RosterRegion
        [TestMethod]
        public void Controller_Portal_Roster_ShouldReturnNewModel()
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

        #region LoginIDNullRegion
        [TestMethod]
        public void Cotroller_Protal_Login_IDIsNull_ShouldReturnRosterPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.Login(id);

            // Assert
            Assert.AreEqual("Roster", result.RouteValues["action"], TestContext.TestName);
        }

        //[TestMethod]
        //public void Cotroller_Protal_Login_ModelIsNull_ShouldReturnRosterPage()
        //{
        //    // Arrange
        //    PortalController controller = new PortalController();
        //    StudentModel data = new StudentModel();
        //    data.AvatarId = null;
        //    string id = Backend.StudentBackend.Instance.Create(data).Id;

        //    // Act
        //    ViewResult result = controller.Login(id) as ViewResult;

        //    var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

        //    // Assert

        //}

        [TestMethod]
        public void Cotroller_Protal_Login_IDValid_Should_Pass()
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

        #region LoginBindRegion
        #endregion

        #region IndexIDNullRegion
        [TestMethod]
        public void Cotroller_Protal_Index_IDIsNull_ShouldReturnRosterPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.Index(id);

            // Assert
            Assert.AreEqual("Roster", result.RouteValues["action"], TestContext.TestName);
        }

        //[TestMethod]
        //public void Cotroller_Protal_Index_ModelIsNull_ShouldReturnRosterPage()
        //{
        //    // Arrange
        //    PortalController controller = new PortalController();
        //    StudentModel data = new StudentModel();
        //    data.AvatarId = null;
        //    string id = Backend.StudentBackend.Instance.Create(data).Id;

        //    // Act
        //    ViewResult result = controller.Index(id) as ViewResult;

        //    var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

        //    // Assert

        //}

        [TestMethod]
        public void Cotroller_Protal_Index_IDValid_Should_Pass()
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

        #region AttendanceIDNullRegion
        [TestMethod]
        public void Cotroller_Protal_Attendance_IDIsNull_ShouldReturnRosterPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.Attendance(id);

            // Assert
            Assert.AreEqual("Roster", result.RouteValues["action"], TestContext.TestName);
        }

        //[TestMethod]
        //public void Cotroller_Protal_Attendance_ModelIsNull_ShouldReturnRosterPage()
        //{
        //    // Arrange
        //    PortalController controller = new PortalController();
        //    StudentModel data = new StudentModel();
        //    data.AvatarId = null;
        //    string id = Backend.StudentBackend.Instance.Create(data).Id;

        //    // Act
        //    ViewResult result = controller.Index(id) as ViewResult;

        //    var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

        //    // Assert

        //}

        [TestMethod]
        public void Cotroller_Protal_Attendance_IDValid_Should_Pass()
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

        #region AvatarBindRegion
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
        public void Cotroller_Protal_House_IDIsNull_ShouldReturnRosterPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.House(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        //[TestMethod]
        //public void Cotroller_Protal_House_ModelIsNull_ShouldReturnRosterPage()
        //{
        //    // Arrange
        //    PortalController controller = new PortalController();
        //    StudentModel data = new StudentModel();
        //    data.AvatarId = null;
        //    string id = Backend.StudentBackend.Instance.Create(data).Id;

        //    // Act
        //    ViewResult result = controller.Index(id) as ViewResult;

        //    var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

        //    // Assert

        //}

        [TestMethod]
        public void Cotroller_Protal_House_IDValid_Should_Pass()
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
        public void Cotroller_Protal_Settings_IDIsNull_ShouldReturnRosterPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.Settings(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        //[TestMethod]
        //public void Cotroller_Protal_Settings_ModelIsNull_ShouldReturnRosterPage()
        //{
        //    // Arrange
        //    PortalController controller = new PortalController();
        //    StudentModel data = new StudentModel();
        //    data.AvatarId = null;
        //    string id = Backend.StudentBackend.Instance.Create(data).Id;

        //    // Act
        //    ViewResult result = controller.Index(id) as ViewResult;

        //    var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

        //    // Assert

        //}

        [TestMethod]
        public void Cotroller_Protal_Settings_IDValid_Should_Pass()
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
        public void Cotroller_Protal_Report_IDIsNull_ShouldReturnRosterPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.Report(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }
        #endregion
    }
}
