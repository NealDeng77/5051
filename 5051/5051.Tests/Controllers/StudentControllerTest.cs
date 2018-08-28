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

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

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
        public void Controller_Student_Read_Get_Id_Is_Null_Should_Return_Error_Page()
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
        public void Controller_Student_Read__Get_myDataStudent_Is_Null_Should_Return_Error_Page()
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
        public void Controller_Student_Create_Post_Model_Is_Invalid_Should_Send_Back_For_Edit()
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
        public void Controller_Student_Create_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            StudentController controller = new StudentController();

            // Act
            var result = (RedirectToRouteResult)controller.Create((StudentModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Create_Post_Id_Is_Null_Or_Empty_Should_Return_Back_For_Edit()
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
        public void Controller_Student_Create_Post_Default_Should_Return_Index_Page()
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
        public void Controller_Student_Update_Get_Id_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            StudentController controller = new StudentController();

            // Act
            var result = (RedirectToRouteResult)controller.Update((string)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Update_Get_myData_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            StudentController controller = new StudentController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            // Act
            var result = (RedirectToRouteResult)controller.Update(id);

            // Reset
            DataSourceBackend.Instance.Reset();

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

        #region UpdatePostRegion

        [TestMethod]
        public void Controller_Student_Update_Post_Model_Is_Invalid_Should_Send_Back_For_Edit()
        {
            // Arrange
            StudentController controller = new StudentController();

            StudentDisplayViewModel data = new StudentDisplayViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Update(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Update_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            StudentController controller = new StudentController();

            // Act
            var result = (RedirectToRouteResult)controller.Update((StudentDisplayViewModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Update_Post_Id_Is_Null_Or_Empty_Should_Send_Back_For_Edit()
        {
            // Arrange
            StudentController controller = new StudentController();

            StudentDisplayViewModel dataNull = new StudentDisplayViewModel();
            StudentDisplayViewModel dataEmpty = new StudentDisplayViewModel();

            // Make data.Id = null
            dataNull.Id = null;

            // Make data.Id empty
            dataEmpty.Id = "";

            // Act
            var resultNull = (ViewResult)controller.Update(dataNull);
            var resultEmpty = (ViewResult)controller.Update(dataEmpty);

            // Assert
            Assert.IsNotNull(resultNull, TestContext.TestName);
            Assert.IsNotNull(resultEmpty, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Update_Post_Default_Should_Return_Index_Page()
        {
            // Arrange
            StudentController controller = new StudentController();

            StudentDisplayViewModel data = new StudentDisplayViewModel(DataSourceBackend.Instance.StudentBackend.GetDefault());

            // Act
            RedirectToRouteResult result = controller.Update(data) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion UpdatePostRegion

        #region DeleteRegion
        [TestMethod]
        public void Controller_Student_Delete_Get_Null_Id_Should_Return_Error()
        {
            // Arrange
            StudentController controller = new StudentController();

            // Act
            var result = (RedirectToRouteResult)controller.Delete((string)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Delete_Invalid_Null_Data_Should_Return_Error()
        {
            // Arrange
            StudentController controller = new StudentController();
            string id = "bogus";

            // Act
            var result = (RedirectToRouteResult)controller.Delete(id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Delete_Get_Default_Should_Pass()
        {
            // Arrange
            StudentController controller = new StudentController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            ViewResult result = controller.Delete(id) as ViewResult;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion DeleteRegion

        #region DeletePostRegion
        [TestMethod]
        public void Controller_Student_Delete_Post_Invalid_Model_Should_Send_Back_For_Edit()
        {
            // Arrange
            StudentController controller = new StudentController();
            StudentDisplayViewModel data = new StudentDisplayViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Delete(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Delete_Post_Null_Data_Should_Return_Error()
        {
            // Arrange
            StudentController controller = new StudentController();

            // Act
            var result = (RedirectToRouteResult)controller.Delete((StudentDisplayViewModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Delete_Post_Null_Id_Should_Send_Back_For_Edit()
        {
            // Arrange
            StudentController controller = new StudentController();
            StudentDisplayViewModel dataNull = new StudentDisplayViewModel();

            // Make data.Id = null
            dataNull.Id = null;

            // Act
            var resultNull = (ViewResult)controller.Delete(dataNull);

            // Assert
            Assert.IsNotNull(resultNull, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Delete_Post_Empty_Id_Should_Send_Back_For_Edit()
        {
            // Arrange
            StudentController controller = new StudentController();
            StudentDisplayViewModel dataEmpty = new StudentDisplayViewModel();

            // Make data.Id empty
            dataEmpty.Id = "";

            // Act
            var resultEmpty = (ViewResult)controller.Delete(dataEmpty);

            // Assert
            Assert.IsNotNull(resultEmpty, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Delete_Post_Default_Should_Return_Index_Page()
        {
            // Arrange
            StudentController controller = new StudentController();

            // Get default student
            StudentModel student = DataSourceBackend.Instance.StudentBackend.GetDefault();
            StudentDisplayViewModel data = new StudentDisplayViewModel(student);

            // Act
            var result = (RedirectToRouteResult)controller.Delete(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }
        #endregion DeletePostRegion
    }
}
