using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Controllers;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class PortalControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Roster
        [TestMethod]
        public void Controller_Portal_Roster_Default_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();

            // Act
            ViewResult result = controller.Roster() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }



        #endregion Instantiate
    }
}
