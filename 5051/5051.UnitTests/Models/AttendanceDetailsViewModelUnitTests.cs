using System;
using _5051.Backend;
using _5051.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class AttendanceDetailsViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void AttendanceDetailsViewModel_Initialize_Invalid_AttendanceId_Should_Fail()
        {
            var backend = StudentBackend.Instance;
            var myAttendanceDetails = new AttendanceDetailsViewModel();

            var result = myAttendanceDetails.Initialize(null);

            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void AttendanceDetailsViewModel_Initialize_Invalid_AttendanceId_Does_Not_Exist_Should_Fail()
        {
            var backend = StudentBackend.Instance;
            var myAttendanceDetails = new AttendanceDetailsViewModel();
            var testAttendance = new AttendanceModel();

            var result = myAttendanceDetails.Initialize(testAttendance.Id);

            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void AttendanceDetailsViewModel_Initialize_Valid_AttendanceId_Should_Pass()
        {
            var backend = StudentBackend.Instance;
            var myAttendanceDetails = new AttendanceDetailsViewModel();
            var testStudent = backend.GetDefault();
            var testAttendance = new AttendanceModel();
            testAttendance.StudentId = testStudent.Id;
            testStudent.Attendance.Add(testAttendance);
            backend.Update(testStudent);


            var result = myAttendanceDetails.Initialize(testAttendance.Id);

            Assert.AreEqual(testStudent.Id, result.Attendance.StudentId, TestContext.TestName);
            Assert.AreEqual(testAttendance.Id, result.Attendance.Id, TestContext.TestName);
        }

    }
}
