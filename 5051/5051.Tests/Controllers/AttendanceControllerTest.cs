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

        #region IndexRegion
        [TestMethod]
        public void Controller_Attendance_Index_Default_Should_Pass()
        {
            // Arrange
            var controller = new AttendanceController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion IndexRegion

        #region ReadStringRegion
        [TestMethod]
        public void Controller_Attendance_Read_Get_Default_Should_Pass()
        {
            // Arrange
            var controller = new AttendanceController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            var myAttendance1 = new AttendanceModel
            {
                StudentId = id,
                Emotion = EmotionStatusEnum.VeryHappy,
                In = DateTime.UtcNow,

                IsNew = false
            };
            var myAttendance2 = new AttendanceModel
            {
                StudentId = id,
                Emotion = EmotionStatusEnum.VeryHappy,
                In = DateTime.UtcNow,
                Out = DateTime.UtcNow,
                IsNew = false
            };


            StudentBackend.Instance.GetDefault().Attendance.Add(myAttendance1);
            StudentBackend.Instance.GetDefault().Attendance.Add(myAttendance2);

            // Act
            var result = controller.Read(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Attendance_Read_Get_Id_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new AttendanceController();

            // Act
            var result = controller.Read(null) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Attendance_Read__Get_myDataAttendance_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new AttendanceController();

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

        #region DetailsStringRegion
        [TestMethod]
        public void Controller_Attendance_Details_Get_Default_Should_Pass()
        {
            // Arrange
            var controller = new AttendanceController();

            var myStudent = StudentBackend.Instance.GetDefault();
            var myAttendance = new AttendanceModel();
            var myId = myAttendance.Id;
            myStudent.Attendance.Add(myAttendance);

            // Act
            var result = controller.Details(myId) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Attendance_Details_Get_Id_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new AttendanceController();

            // Act
            var result = controller.Details(null) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        #endregion ReadStringRegion

        #region CreateRegion

        [TestMethod]
        public void Controller_Attendance_Create_Default_Should_Pass()
        {
            // Arrange
            var controller = new AttendanceController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            var result = controller.Create(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion CreateRegion

        #region CreatePostRegion

        [TestMethod]
        public void Controller_Attendance_Create_Post_Model_Is_Invalid_Should_Send_Back_For_Edit()
        {
            // Arrange
            AttendanceController controller = new AttendanceController();

            AttendanceModel data = new AttendanceModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Create(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Attendance_Create_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            AttendanceController controller = new AttendanceController();

            // Act
            var result = (RedirectToRouteResult)controller.Create((AttendanceModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Attendance_Create_Post_Id_Is_Null_Or_Empty_Should_Return_Back_For_Edit()
        {
            // Arrange
            AttendanceController controller = new AttendanceController();

            AttendanceModel dataNull = new AttendanceModel();
            AttendanceModel dataEmpty = new AttendanceModel();

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
        public void Controller_Attendance_Create_Post_Default_Should_Return_Read_Page()
        {
            // Arrange
            AttendanceController controller = new AttendanceController();

            var attendanceModel = new AttendanceModel
            {
                StudentId = StudentBackend.Instance.GetDefault().Id
            };

            // Act
            var result = (RedirectToRouteResult)controller.Create(attendanceModel);

            // Assert
            Assert.AreEqual("Read", result.RouteValues["action"], TestContext.TestName);
        }



        #endregion CreatePostRegion

        #region UpdateRegion

        [TestMethod]
        public void Controller_Attendance_Update_Get_Id_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            AttendanceController controller = new AttendanceController();

            // Act
            var result = (RedirectToRouteResult)controller.Update((string)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Attendance_Update_Get_myData_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            AttendanceController controller = new AttendanceController();


            // Act
            var result = (RedirectToRouteResult)controller.Update("bogus");

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Attendance_Update_Get_Default_Should_Pass()
        {
            // Arrange
            var controller = new AttendanceController();

            var myStudent = StudentBackend.Instance.GetDefault();
            var myAttendance = new AttendanceModel();
            var myId = myAttendance.Id;
            myStudent.Attendance.Add(myAttendance);

            // Act
            var result = controller.Update(myId) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion UpdateRegion

        #region UpdatePostRegion

        [TestMethod]
        public void Controller_Attendance_Update_Post_Model_Is_Invalid_Should_Send_Back_For_Edit()
        {
            // Arrange
            AttendanceController controller = new AttendanceController();

            AttendanceModel data = new AttendanceModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Update(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Attendance_Update_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            AttendanceController controller = new AttendanceController();

            // Act
            var result = (RedirectToRouteResult)controller.Update((AttendanceModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Attendance_Update_Post_Id_Is_Null_Or_Empty_Should_Send_Back_For_Edit()
        {
            // Arrange
            AttendanceController controller = new AttendanceController();

            AttendanceModel dataNull = new AttendanceModel();
            AttendanceModel dataEmpty = new AttendanceModel();

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
        public void Controller_Attendance_Update_Post_Id_Is_Invalid_Should_Return_Error_Page()
        {
            var controller = new AttendanceController();

            var myStudent = StudentBackend.Instance.GetDefault();
            var myAttendance = new AttendanceModel();
            myStudent.Attendance.Add(myAttendance);

            var myData = new AttendanceModel();
            myData.StudentId = myAttendance.Id;
            myData.Id = "bogus";
            // Act
            var result = (RedirectToRouteResult)controller.Update(myData);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Attendance_Update_Post_Default_Should_Return_Details_Page()
        {
            // Arrange
            var controller = new AttendanceController();

            var myStudent = StudentBackend.Instance.GetDefault();
            var myAttendance = new AttendanceModel();
            myStudent.Attendance.Add(myAttendance);

            // Act
            RedirectToRouteResult result = controller.Update(myAttendance) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Details", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion UpdatePostRegion

        #region DeleteRegion
        [TestMethod]
        public void Controller_Attendance_Delete_Get_Null_Id_Should_Return_Error()
        {
            // Arrange
            AttendanceController controller = new AttendanceController();

            // Act
            var result = (RedirectToRouteResult)controller.Delete((string)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Attendance_Delete_Invalid_Null_Data_Should_Return_Error()
        {
            // Arrange
            AttendanceController controller = new AttendanceController();
            string id = "bogus";

            // Act
            var result = (RedirectToRouteResult)controller.Delete(id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }
    }
}
