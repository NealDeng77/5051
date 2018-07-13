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
    public class StudentReportViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_StudentReportViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new StudentReportViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentReportViewModel_Default_Instantiate_Get_Set_Data_Should_Pass()
        {
            //arrange
            var result = new StudentReportViewModel();
            var expectStudentId = "GoodStudentID";
            var expectYear = 2018;
            var expectMonth = 4;
            var expectStudent = new StudentModel();
            var expectDateStart = new DateTime();
            var expectDateEnd = DateTime.UtcNow;

            // Act
            result.StudentId = expectStudentId;
            result.Year = expectYear;
            result.Month = expectMonth;
            result.Student = expectStudent;
            result.DateStart = expectDateStart;
            result.DateEnd = expectDateEnd;

            // Assert
            Assert.AreEqual(expectStudentId, result.StudentId, TestContext.TestName);
            Assert.AreEqual(expectYear, result.Year, TestContext.TestName);
            Assert.AreEqual(expectMonth, result.Month, TestContext.TestName);
            Assert.AreEqual(expectStudent, result.Student, TestContext.TestName);
            Assert.AreEqual(expectDateStart, result.DateStart, TestContext.TestName);
            Assert.AreEqual(expectDateEnd, result.DateEnd, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
