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
    public class CalendarControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_Calendar_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new CalendarController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new CalendarController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region IndexRegion

        [TestMethod]
        public void Controller_Calendar_Index_Default_Should_Pass()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion IndexRegion
    }
}
