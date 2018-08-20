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
    }
}
