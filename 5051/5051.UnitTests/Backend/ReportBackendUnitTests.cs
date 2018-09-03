using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class ReportBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

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

            var dayNow = UTCConversionsBackend.UtcToKioskTime(DateTime.UtcNow).Date; //today's date

            var thisMonday = dayNow.AddDays(-7-((dayNow.DayOfWeek - DayOfWeek.Monday + 7) % 7)); //this Monday's date

            var attendanceMon = new AttendanceModel
            {
                In = UTCConversionsBackend.KioskTimeToUtc(thisMonday.AddHours(8)),
                Out = UTCConversionsBackend.KioskTimeToUtc(thisMonday.AddHours(9)),
                Emotion = EmotionStatusEnum.VeryHappy
            };
            var attendanceTue = new AttendanceModel
            {
                In = UTCConversionsBackend.KioskTimeToUtc(thisMonday.AddDays(1).AddHours(10)),
                Out = UTCConversionsBackend.KioskTimeToUtc(thisMonday.AddDays(1).AddHours(12)),
                Emotion = EmotionStatusEnum.Happy
            };
            var attendanceWed = new AttendanceModel
            {
                In = UTCConversionsBackend.KioskTimeToUtc(thisMonday.AddDays(2).AddHours(10)),
                Out = UTCConversionsBackend.KioskTimeToUtc(thisMonday.AddDays(2).AddHours(12)),
                Emotion = EmotionStatusEnum.Neutral
            };
            var attendanceThu = new AttendanceModel
            {
                In = UTCConversionsBackend.KioskTimeToUtc(thisMonday.AddDays(3).AddHours(10)),
                Out = UTCConversionsBackend.KioskTimeToUtc(thisMonday.AddDays(3).AddHours(12)),
                Emotion = EmotionStatusEnum.Sad
            };
            var attendanceFri = new AttendanceModel
            {
                In = UTCConversionsBackend.KioskTimeToUtc(thisMonday.AddDays(4).AddHours(10)),
                Out = UTCConversionsBackend.KioskTimeToUtc(thisMonday.AddDays(4).AddHours(12)),
                Emotion = EmotionStatusEnum.VerySad
            };

            testStudent.Attendance.Add(attendanceMon);
            testStudent.Attendance.Add(attendanceTue);
            testStudent.Attendance.Add(attendanceWed);
            testStudent.Attendance.Add(attendanceThu);
            testStudent.Attendance.Add(attendanceFri);

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
            var testStudentAttendance1 = new AttendanceModel
            {
                In = new DateTime(2018, 1, 15)
            };
            testStudent.Attendance.Add(testStudentAttendance1);
            var testStudentAttendance2 = new AttendanceModel
            {
                In = DateTime.UtcNow
            };
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
            var testStudentAttendance1 = new AttendanceModel
            {
                In = _5051.Backend.UTCConversionsBackend.KioskTimeToUtc(new DateTime(2018, 1, 15)),
                Out = _5051.Backend.UTCConversionsBackend.KioskTimeToUtc(new DateTime(2018, 1, 15, 23, 59, 59))
            };
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
            var testStudentAttendance1 = new AttendanceModel
            {
                In = _5051.Backend.UTCConversionsBackend.KioskTimeToUtc(new DateTime(2018, 1, 15, 12, 0, 0))
            };
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

        [TestMethod]
        public void Backend_ReportBackend_GenerateLeaderboard_Valid_Data_Should_Pass()
        {
            //arrange
            var studentList = StudentBackend.Instance.Index();

            var dayNow = UTCConversionsBackend.UtcToKioskTime(DateTime.UtcNow).Date; //today's date

            var thisMonday = dayNow.AddDays(-((dayNow.DayOfWeek - DayOfWeek.Monday + 7) % 7)); //this Monday's date

            var attendanceMon = new AttendanceModel
            {
                In = UTCConversionsBackend.KioskTimeToUtc(thisMonday.AddHours(8)),
                Out = UTCConversionsBackend.KioskTimeToUtc(thisMonday.AddHours(9)),
            };
            var attendanceTue = new AttendanceModel
            {
                In = UTCConversionsBackend.KioskTimeToUtc(thisMonday.AddHours(10)),
                Out = UTCConversionsBackend.KioskTimeToUtc(thisMonday.AddHours(12)),
            };

            var student1 = studentList[0];
            student1.Attendance.Add(attendanceMon);
            StudentBackend.Instance.Update(student1);

            var student2 = studentList[1];
            student2.Attendance.Add(attendanceMon);
            student2.Attendance.Add(attendanceTue);
            StudentBackend.Instance.Update(student2);

            //act
            var result = ReportBackend.Instance.GenerateLeaderboard();

            //reset
            StudentBackend.Instance.Reset();

            //assert
            Assert.AreEqual(student1.Id, result[1].Id, TestContext.TestName);
            Assert.AreEqual(student2.Id, result[0].Id, TestContext.TestName);
        }
    }
}
