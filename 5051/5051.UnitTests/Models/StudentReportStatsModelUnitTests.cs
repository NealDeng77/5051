using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class StudentReportStatsModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_StudentReportStatsModel_Default_Instantiate_Get_Set_Should_Pass()
        {
            //arrange
            var test = new StudentReportStatsModel();
            var expectAccumulatedTotalHours = TimeSpan.MinValue;
            var expectAccumulatedTotalHoursExpected = TimeSpan.MaxValue;
            var expectDaysPresent = 1;
            var expectDaysAbsentExcused = 2;
            var expectDaysAbsentUnexcused = 3;
            var expectTotalHoursAttended = 4;
            var expectTotalHoursMissing = 5;
            var expectDaysOnTime = 6;
            var expectDaysLate = 7;
            var expectDaysStayed = 8;
            var expectDaysLeftEarly = 9;
            var expectDaysOnTimeStayed = 10;
            var expectDaysOnTimeLeft = 11;
            var expectDaysLateStayed = 12;
            var expectDaysLateLeft = 13;
            var expectPercPresent = 100 * expectDaysPresent / (expectDaysPresent + expectDaysAbsentExcused + expectDaysAbsentUnexcused);
            var expectPercAttendedHours = 100 * expectTotalHoursAttended / (expectTotalHoursAttended + expectTotalHoursMissing);
            var expectPercExcused = 100 * expectDaysAbsentExcused / (expectDaysPresent + expectDaysAbsentExcused + expectDaysAbsentUnexcused);
            var expectPercUnexcused = 100 * expectDaysAbsentUnexcused / (expectDaysPresent + expectDaysAbsentExcused + expectDaysAbsentUnexcused);

            //act
            test.AccumlatedTotalHours = expectAccumulatedTotalHours;
            test.AccumlatedTotalHoursExpected = expectAccumulatedTotalHoursExpected;
            test.DaysPresent = expectDaysPresent;
            test.DaysAbsentExcused = expectDaysAbsentExcused;
            test.DaysAbsentUnexcused = expectDaysAbsentUnexcused;
            test.TotalHoursAttended = expectTotalHoursAttended;
            test.TotalHoursMissing = expectTotalHoursMissing;
            test.DaysOnTime = expectDaysOnTime;
            test.DaysLate = expectDaysLate;
            test.DaysStayed = expectDaysStayed;
            test.DaysLeftEarly = expectDaysLeftEarly;
            test.DaysOnTimeStayed = expectDaysOnTimeStayed;
            test.DaysOnTimeLeft = expectDaysOnTimeLeft;
            test.DaysLateStayed = expectDaysLateStayed;
            test.DaysLateLeft = expectDaysLateLeft;
            test.PercPresent = expectPercPresent;
            test.PercAttendedHours = expectPercAttendedHours;
            test.PercExcused = expectPercExcused;
            test.PercUnexcused = expectPercUnexcused;

            //assert
            Assert.AreEqual(expectAccumulatedTotalHours, test.AccumlatedTotalHours, TestContext.TestName);
            Assert.AreEqual(expectAccumulatedTotalHoursExpected, test.AccumlatedTotalHoursExpected, TestContext.TestName);
            Assert.AreEqual(expectDaysPresent, test.DaysPresent, TestContext.TestName);
            Assert.AreEqual(expectDaysAbsentExcused, test.DaysAbsentExcused, TestContext.TestName);
            Assert.AreEqual(expectDaysAbsentUnexcused, test.DaysAbsentUnexcused, TestContext.TestName);
            Assert.AreEqual(expectTotalHoursAttended, test.TotalHoursAttended, TestContext.TestName);
            Assert.AreEqual(expectTotalHoursMissing, test.TotalHoursMissing, TestContext.TestName);
            Assert.AreEqual(expectDaysOnTime, test.DaysOnTime, TestContext.TestName);
            Assert.AreEqual(expectDaysLate, test.DaysLate, TestContext.TestName);
            Assert.AreEqual(expectDaysStayed, test.DaysStayed, TestContext.TestName);
            Assert.AreEqual(expectDaysLeftEarly, test.DaysLeftEarly, TestContext.TestName);
            Assert.AreEqual(expectDaysOnTimeStayed, test.DaysOnTimeStayed, TestContext.TestName);
            Assert.AreEqual(expectDaysOnTimeLeft, test.DaysOnTimeLeft, TestContext.TestName);
            Assert.AreEqual(expectDaysLateStayed, test.DaysLateStayed, TestContext.TestName);
            Assert.AreEqual(expectDaysLateLeft, test.DaysLateLeft, TestContext.TestName);
            Assert.AreEqual(expectPercPresent, test.PercPresent, TestContext.TestName);
            Assert.AreEqual(expectPercAttendedHours, test.PercAttendedHours, TestContext.TestName);
            Assert.AreEqual(expectPercExcused, test.PercExcused, TestContext.TestName);
            Assert.AreEqual(expectPercUnexcused, test.PercUnexcused, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
