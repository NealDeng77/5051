using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class ReportBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Backend_ReportBackend_GenerateOverallReport_Valid_Report_Should_Pass()
        {
            //arrange
            var reportBackend = ReportBackend.Instance;
            var testReport = new MonthlyReportViewModel();
            var studentBackend = StudentBackend.Instance;
            var testStudent = studentBackend.GetDefault();
            testReport.Student = testStudent;
            testReport.StudentId = testStudent.Id;
            var testStudentAttendance1 = new AttendanceModel();
            testStudentAttendance1.In = new DateTime(2018, 1, 15);
            testStudent.Attendance.Add(testStudentAttendance1);
            var testStudentAttendance2 = new AttendanceModel();
            testStudentAttendance2.In = DateTime.UtcNow;
            testStudent.Attendance.Add(testStudentAttendance2);
            testReport.Stats.DaysPresent = 2;
            testReport.DateEnd = DateTime.UtcNow;

            //act
            var result = reportBackend.GenerateOverallReport(testReport);

            //reset
            studentBackend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_ReportBackend_GenerateMonthlyReport_Valid_Report_Should_Pass()
        {
            //arrange
            var reportBackend = ReportBackend.Instance;
            var testReport = new MonthlyReportViewModel();
            var studentBackend = StudentBackend.Instance;
            var testStudent = studentBackend.GetDefault();
            testReport.Student = testStudent;
            testReport.StudentId = testStudent.Id;
            var testStudentAttendance1 = new AttendanceModel();
            testStudentAttendance1.In = new DateTime(2018, 1, 15);
            testStudent.Attendance.Add(testStudentAttendance1);
            var testStudentAttendance2 = new AttendanceModel();
            testStudentAttendance2.In = DateTime.UtcNow;
            testStudent.Attendance.Add(testStudentAttendance2);
            testReport.Stats.DaysPresent = 2;
            testReport.DateEnd = DateTime.UtcNow;

            
            //act
            var result = reportBackend.GenerateMonthlyReport(testReport);

            //reset
            //reportBackend.Reset();
            studentBackend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }


        [TestMethod]
        public void Backend_ReportBackend_CalculateDurationInOutStatus_Valid_Report_CheckOut_DoneAuto_Should_Pass()
        {
            //arrange
            var reportBackend = ReportBackend.Instance;
            var testReport = new MonthlyReportViewModel();
            var studentBackend = StudentBackend.Instance;
            var testStudent = studentBackend.GetDefault();
            testReport.Student = testStudent;
            testReport.StudentId = testStudent.Id;
            var testStudentAttendance1 = new AttendanceModel();
            testStudentAttendance1.In = new DateTime(2018, 1, 15);
            testStudentAttendance1.Out = new DateTime(2018, 1, 15, 23, 59, 59);
            testStudent.Attendance.Add(testStudentAttendance1);
            testReport.Stats.DaysPresent = 1;
            testReport.DateEnd = DateTime.UtcNow;

            //act
            var result = reportBackend. GenerateOverallReport(testReport);

            //reset
            studentBackend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_ReportBackend_CalculateDurationInOutStatus_Valid_Report_CheckIn_Late_Should_Pass()
        {
            //arrange
            var reportBackend = ReportBackend.Instance;
            var testReport = new MonthlyReportViewModel();
            var studentBackend = StudentBackend.Instance;
            var testStudent = studentBackend.GetDefault();
            testReport.Student = testStudent;
            testReport.StudentId = testStudent.Id;
            var testStudentAttendance1 = new AttendanceModel();
            testStudentAttendance1.In = new DateTime(2018, 1, 15, 12, 0, 0);
            testStudent.Attendance.Add(testStudentAttendance1);
            testReport.Stats.DaysPresent = 1;
            testReport.DateEnd = DateTime.UtcNow;

            //act
            var result = reportBackend.GenerateOverallReport(testReport);

            //reset
            studentBackend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
    }
}
