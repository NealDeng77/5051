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
    public class StudentControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_Student_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new StudentController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new StudentController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region IndexRegion
        [TestMethod]
        public void Controller_Student_Index_Default_Should_Pass()
        {
            // Arrange
            var controller = new StudentController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion IndexRegion

        #region ReadStringRegion
        [TestMethod]
        public void Controller_Student_Read_Get_Default_Should_Pass()
        {
            // Arrange
            var controller = new StudentController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            var result = controller.Read(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Read_Get_IdIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            var controller = new StudentController();

            // Act
            var result = controller.Read(null) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Read__Get_myDataStudentIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            var controller = new StudentController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Act
            var result = (RedirectToRouteResult)controller.Read(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        #endregion ReadStringRegion

        #region CreateRegion

        [TestMethod]
        public void Controller_Student_Create_Default_Should_Pass()
        {
            // Arrange
            var controller = new StudentController();

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion CreateRegion

        #region CreatePostRegion

        [TestMethod]
        public void Controller_Student_Create_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            StudentController controller = new StudentController();

            StudentModel data = new StudentModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Create(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Create_Post_DataIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            StudentController controller = new StudentController();

            // Act
            var result = (RedirectToRouteResult)controller.Create((StudentModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Create_Post_IdIsNullOrEmpty_Should_Pass()
        {
            // Arrange
            StudentController controller = new StudentController();

            StudentModel dataNull = new StudentModel();
            StudentModel dataEmpty = new StudentModel();

            // Make data.Id = null
            dataNull.Id = null;

            // Make data.Id empty
            dataEmpty.Id = "";

            // Act
            var resultNull = (ViewResult)controller.Create(dataNull);
            var resultEmpty = (ViewResult)controller.Create(dataEmpty);

            // Assert
            Assert.IsNotNull(resultNull, TestContext.TestName);
            Assert.IsNotNull(resultEmpty, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Create_Post_Default_ShouldReturnIndexPage()
        {
            // Arrange
            StudentController controller = new StudentController();

            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();

            // Act
            var result = (RedirectToRouteResult)controller.Create(data);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion CreatePostRegion

        #region UpdateRegion

        [TestMethod]
        public void Controller_Student_Update_Get_IdIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            StudentController controller = new StudentController();

            // Act
            var result = (RedirectToRouteResult)controller.Update((string)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Update_Get_myDataIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            StudentController controller = new StudentController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Act
            var result = (RedirectToRouteResult)controller.Update(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Update_Get_Default_Should_Pass()
        {
            // Arrange
            StudentController controller = new StudentController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            ViewResult result = controller.Update(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion UpdateRegion

    }
}
