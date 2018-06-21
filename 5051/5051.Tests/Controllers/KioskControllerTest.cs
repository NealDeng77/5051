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
    public class KioskControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_Kiosk_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new KioskController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new KioskController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region IndexRegion
        [TestMethod]
        public void Controller_Kiosk_Index_Default_Should_Pass()
        {
            // Arrange
            KioskController controller = new KioskController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_Index_With_Empty_List_Should_Return_Error_Page()
        {
            // Arrange
            KioskController controller = new KioskController();
            
            // Set unitesting backend data
            DataSourceBackend.Instance.SetDataSourceDataSet(DataSourceDataSetEnum.UnitTest);

            // Make empty StudentList
            while (DataSourceBackend.Instance.StudentBackend.Index().Count != 0)
            {
                var first = DataSourceBackend.Instance.StudentBackend.GetDefault();
                DataSourceBackend.Instance.StudentBackend.Delete(first.Id);
            }

            // Act
            var result = (RedirectToRouteResult)controller.Index();

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["route"], TestContext.TestName);
        }
        #endregion IndexRegion
    }
}
