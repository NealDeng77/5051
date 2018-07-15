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
        public void Backend_ReportBackend_GenerateOVerallReport_Valid_Report_Should_Pass()
        {
            //arrange
            var reportBackend = ReportBackend.Instance;
            var testReport = new StudentReportViewModel();
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
            //reportBackend.Reset();
            studentBackend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }


        [TestMethod]
        public void Backend_ReportBackend_GenerateMonthlyReport_Valid_Report_Pass()
        {
            //arrange
            var reportBackend = ReportBackend.Instance;
            var testReport = new StudentReportViewModel();
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
            testReport.Year = DateTime.UtcNow.Year;
            testReport.Month = DateTime.UtcNow.Month;
            
            //act
            var result = reportBackend.GenerateMonthlyReport(testReport);

            //reset
            //reportBackend.Reset();
            studentBackend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
    }
}
