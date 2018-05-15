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
    public class HomeControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Controller_Home_Index_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Home_About_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Home_Contact_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Home_Privacy_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Home_ShopExample_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.ShopExample() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Home_StudentExample_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.StudentExample() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Home_HouseExample_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.HouseExample() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

    }
}
