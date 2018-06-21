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

        [TestMethod]
        public void Controller_Calendar_Index_With_Empty_List_Should_Return_Error_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            // Set unit testing backend data
            DataSourceBackend.Instance.SetDataSourceDataSet(DataSourceDataSetEnum.UnitTest);

            // Make empty StudentList
            while (DataSourceBackend.Instance.SchoolCalendarBackend.Index().Count != 0)
            {
                var first = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault();
                DataSourceBackend.Instance.SchoolCalendarBackend.Delete(first.Id);
            }

            // Act
            var result = (RedirectToRouteResult)controller.Index();

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        #endregion IndexRegion

        #region SetDefaultRegion

        [TestMethod]
        public void Controller_Calendar_SetDefault_IdIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            // Act
            var result = (RedirectToRouteResult)controller.SetDefault(null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_SetDefault_myDataIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Act
            var result = (RedirectToRouteResult)controller.SetDefault(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_SetDefault_ShouldReturnCalendarPage()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            // Act
            var result = (RedirectToRouteResult)controller.SetDefault(id);

            // Assert
            Assert.AreEqual("Update", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Calendar", result.RouteValues["controller"], TestContext.TestName);
        }

        #endregion SetDefaultRegion

        #region SetEarlyEndRegion

        [TestMethod]
        public void Controller_Calendar_SetEarlyEnd_IdIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            // Act
            var result = (RedirectToRouteResult)controller.SetEarlyEnd(null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_SetEarlyEnd_myDataIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Act
            var result = (RedirectToRouteResult)controller.SetEarlyEnd(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_SetEarlyEnd_ShouldReturnCalendarPage()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            // Act
            var result = (RedirectToRouteResult)controller.SetEarlyEnd(id);

            // Assert
            Assert.AreEqual("Update", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Calendar", result.RouteValues["controller"], TestContext.TestName);
        }

        #endregion SetEarlyEndRegion
    }
}
