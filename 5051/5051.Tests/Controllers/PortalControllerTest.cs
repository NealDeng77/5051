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
    public class PortalControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Roster
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

        #region Login(String)

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

        #region Group
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
