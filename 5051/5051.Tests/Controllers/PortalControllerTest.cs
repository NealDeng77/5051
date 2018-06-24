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
        //    string id = Backend.StudentBackend.Instance.Create(data).Id;

        //    // Act
        //    ViewResult result = controller.Login(id) as ViewResult;
            

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

        #region Login(bind)
        #endregion

        #region index
        #endregion

        #region Attendance
        #endregion

        #region Avatar(bind)
        #endregion

        #region Avatar(string)
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

        #region House
        #endregion

        #region Settings(string)
        #endregion

        #region Settings(bind)
        #endregion

        #region Report
        #endregion





    }
}
