using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Backend;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class BaseReportViewModelUnitTests
    {
        public TestContext TestContext { get; set; }
        #region Instantiate
        [TestMethod]
        public void Models_BaseReportViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new BaseReportViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate
        #region Get_Set_All_Fields
        [TestMethod]
        public void Models_BaseReportViewModel_Get_Set_Check_All_Fields_Should_Pass()
        {
            // Arrange




            // Act
            // Set all the fields for a BaseReportViewModel
            var test = new BaseReportViewModel();
            test.StudentId = StudentBackend.Instance.GetDefault().Id;
            test.Student = StudentBackend.Instance.GetDefault();
            test.DateStart = new DateTime(2000, 12, 30);
            test.DateEnd = new DateTime(2018, 8, 7);
            test.AttendanceList = new List<AttendanceReportViewModel>();
            test.Stats = new StudentReportStatsModel();
            test.Goal = 100;

            var expectedStudentId = StudentBackend.Instance.GetDefault().Id;
            var expectedStudent = StudentBackend.Instance.GetDefault();
            var expectedDateStart = new DateTime(2000, 12, 30);
            var expectedDateEnd = new DateTime(2018, 8, 7);
            var expectedGoal = 100;
            // Assert

            //Check each value
            Assert.AreEqual(test.StudentId, expectedStudentId, "StudentId " + TestContext.TestName);
            Assert.AreEqual(test.Student, expectedStudent, "Student " + TestContext.TestName);
            Assert.AreEqual(test.DateStart, expectedDateStart, "DateStart " + TestContext.TestName);
            Assert.AreEqual(test.DateEnd, expectedDateEnd, "DateEnd " + TestContext.TestName);
            Assert.AreEqual(test.Goal, expectedGoal, "Goal " + TestContext.TestName);
            Assert.IsNotNull(test.AttendanceList, "AttendanceList " + TestContext.TestName);
            Assert.IsNotNull(test.Stats, "Stats " + TestContext.TestName);
        }
        #endregion Get_Set_All_Fields
    }
}
