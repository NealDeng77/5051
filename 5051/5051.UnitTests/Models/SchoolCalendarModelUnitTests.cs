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
    public class SchoolCalendarModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_SchoolCalendarModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new SchoolCalendarModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SchoolCalendarModel_Default_Instantiate_With_Data_Should_Pass()
        {
            var test = new SchoolCalendarModel();
            test.Modified = true;

            // Act
            var result = new SchoolCalendarModel(test);
            var expect = test.Modified;

            // Assert
            Assert.AreEqual(expect, result.Modified, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SchoolCalendarModel_Default_Instantiate_With_Invalid_Data_Should_Fail()
        {
            // Act
            var result = new SchoolCalendarModel(null);
            string expect = null;

            // Assert
            Assert.AreEqual(expect, result.Id, TestContext.TestName);
        }
        #endregion Instantiate

        #region SetSchoolTime
        [TestMethod]
        public void Models_SchoolCalendarModel_SetSchoolTime_Start_Early_Should_Pass()
        {
            // Arrange
            var data = new SchoolCalendarModel();
            data.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
            var expect = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().StartEarly;

            // Act
            data.SetSchoolTime();
            var result = data.TimeStart;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SchoolCalendarModel_SetSchoolTime_Start_Late_Should_Pass()
        {
            // Arrange
            var data = new SchoolCalendarModel();
            data.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Late;
            var expect = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().StartLate;

            // Act
            data.SetSchoolTime();
            var result = data.TimeStart;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        
        [TestMethod]
        public void Models_SchoolCalendarModel_SetSchoolTime_Start_Normal_Should_Pass()
        {
            // Arrange
            var data = new SchoolCalendarModel();
            data.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Normal;
            var expect = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().StartNormal;

            // Act
            data.SetSchoolTime();
            var result = data.TimeStart;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SchoolCalendarModel_SetSchoolTime_End_Early_Should_Pass()
        {
            // Arrange
            var data = new SchoolCalendarModel();
            data.DayEnd = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
            var expect = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().EndEarly;

            // Act
            data.SetSchoolTime();
            var result = data.TimeEnd;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SchoolCalendarModel_SetSchoolTime_End_Late_Should_Pass()
        {
            // Arrange
            var data = new SchoolCalendarModel();
            data.DayEnd = _5051.Models.Enums.SchoolCalendarDismissalEnum.Late;
            var expect = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().EndLate;

            // Act
            data.SetSchoolTime();
            var result = data.TimeEnd;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SchoolCalendarModel_SetSchoolTime_End_Normal_Should_Pass()
        {
            // Arrange
            var data = new SchoolCalendarModel();
            data.DayEnd = _5051.Models.Enums.SchoolCalendarDismissalEnum.Normal;
            var expect = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().EndNormal;

            // Act
            data.SetSchoolTime();
            var result = data.TimeEnd;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion SetScoolTime
    }
}
