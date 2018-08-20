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
    public class AttendanceControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_Attendance_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new AttendanceController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new AttendanceController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate


    }
}
