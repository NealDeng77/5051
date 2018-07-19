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
    public class StudentModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_StudentModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new StudentModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentModel_Instantiate_Valid_Data_Should_Pass()
        {
            // Arrange
            var avatarId = "avatarid";
            var name = "name";

            var expect = avatarId;

            // Act
            var result = new StudentModel(name, avatarId);

            // Assert
            Assert.AreEqual(expect,result.AvatarId, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentModel_Instantiate_InValid_Data_Should_Fail()
        {
            // Arrange
            string avatarId = null;
            var name = "name";

            var expect = Backend.AvatarBackend.Instance.GetFirstAvatarId();

            // Act
            var result = new StudentModel(name, avatarId);

            // Assert
            Assert.AreEqual(expect, result.AvatarId, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentModel_Instantiate_Valid_Model_Data_Should_Pass()
        {
            // Arrange
            var data = new StudentDisplayViewModel();
            data.AvatarId = "avatarID";
            var expect = "avatarID";
                
            // Act
            var result = new StudentModel(data);

            // Assert
            Assert.AreEqual(expect, result.AvatarId, TestContext.TestName);
        }

        #endregion Instantiate

        #region Update
        [TestMethod]
        public void Models_StudentModel_Update_With_Valid_Data_Should_Pass()
        {
            // Arrange
            var expect = "test";

            var data = new StudentModel();
            data.AvatarId = "bogus";

            var test = new StudentModel();
            test.AvatarId = "test";

            // Act
            data.Update(test);
            var result = data.AvatarId;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentModel_Update_Check_All_Fields_Should_Pass()
        {
            // Arrange

            // Set all the fields for a Student
            var data = new StudentModel();

            var test = new StudentModel();
            test.Id = "Id";
            test.Name = "Name";
            test.Password = "Password";
            test.Status = StudentStatusEnum.Hold;
            test.Tokens = 1000;
            test.ExperiencePoints = 1000;
            test.EmotionCurrent = EmotionStatusEnum.Happy;

            test.AvatarId = "AvatarId";
            test.AvatarLevel = 10;

            var tempAttendance = new AttendanceModel();
            test.Attendance.Add(tempAttendance);

            var tempInventory = new FactoryInventoryModel();
            test.Inventory.Add(tempInventory);

            // Act
            data.Update(test);

            // Assert

            // Id should not change...
            Assert.AreNotEqual(test.Id, data.Id, "Name " + TestContext.TestName);

            //Check each value
            Assert.AreEqual(test.Name, data.Name, "Name "+ TestContext.TestName);
            Assert.AreEqual(test.Password, data.Password, "Password " + TestContext.TestName);
            Assert.AreEqual(test.Status, data.Status, "Status " + TestContext.TestName);
            Assert.AreEqual(test.Tokens, data.Tokens, "Tokens " + TestContext.TestName);
            Assert.AreEqual(test.ExperiencePoints, data.ExperiencePoints, "ExperiencePoints " + TestContext.TestName);
            Assert.AreEqual(test.EmotionCurrent, data.EmotionCurrent, "EmotionCurrent " + TestContext.TestName);
            Assert.AreEqual(test.AvatarId, data.AvatarId, "AvatarId " + TestContext.TestName);
            Assert.AreEqual(test.AvatarLevel, data.AvatarLevel, "AvatarLevel " + TestContext.TestName);
            Assert.AreEqual(test.Attendance.Count, data.Attendance.Count, "Attendance " + TestContext.TestName);
            Assert.AreEqual(test.Inventory.Count, data.Inventory.Count, "Inventory " + TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentModel_Update_With_Invalid_Data_Null_Should_Fail()
        {
            // Arrange

            var expect = "test";

            var data = new StudentModel();
            data.Id = "test";

            // Act
            data.Update(null);
            var result = data.Id;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        #endregion Update
    }
}
