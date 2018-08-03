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
    public class StudentDisplayViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new StudentDisplayViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_With_Data_Should_Pass()
        {
            // Arrange
            var data = new StudentModel();
            data.Id = "hi";
            var expect = "hi";

            // Act
            var returned = new StudentDisplayViewModel(data);
            var result = returned.Id;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_With_Invalid_Data_Null_Should_Fail()
        {
            // Arrange

            // Act
            var result = new StudentDisplayViewModel(null);
      
            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_With_Invalid_Data_AvatarID_Null_Should_Fail()
        {
            // Arrange
            var data = new StudentModel();
            data.Id = "hi";
            data.AvatarId = null;
            
            // Act
            var result = new StudentDisplayViewModel(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_With_Valid_Data_Should_Pass()
        {
            // Arrange

            var dataAvatar = new AvatarModel();
            Backend.DataSourceBackend.Instance.AvatarBackend.Create(dataAvatar);

            var data = new StudentModel();
            data.Id = "hi";
            data.AvatarId = dataAvatar.Id;

            // Act
            var result = new StudentDisplayViewModel(data);


            Backend.DataSourceBackend.Instance.AvatarBackend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_Get_Valid_Data_Should_Pass()
        {
            // Arrange
            var result = new StudentDisplayViewModel();

            //act
            var expectUri = result.AvatarUri;
            var expectAvatarName = result.AvatarName;
            var expectAvatarDesc = result.AvatarDescription;
            var expectLastDateTime = result.LastDateTime;

            // Assert
            Assert.AreEqual(expectUri, result.AvatarUri, TestContext.TestName);
            Assert.AreEqual(expectAvatarName, result.AvatarName, TestContext.TestName);
            Assert.AreEqual(expectAvatarDesc, result.AvatarDescription, TestContext.TestName);
            Assert.AreEqual(expectLastDateTime, result.LastDateTime, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_Set_LastDateTime_Should_Pass()
        {
            // Arrange
            var result = new StudentDisplayViewModel();
            var expectLastDateTime = DateTime.UtcNow;

            //act
            result.LastDateTime = expectLastDateTime;

            // Assert
            Assert.AreEqual(expectLastDateTime, result.LastDateTime, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_Set_LastLogin_Should_Pass()
        {
            // Arrange
            var result = new StudentDisplayViewModel();
            var expectLastLogin= DateTime.UtcNow;

            //act
            result.LastLogIn = expectLastLogin;

            // Assert
            Assert.AreEqual(expectLastLogin, result.LastLogIn, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_Set_EmotionImgeUri_Should_Pass()
        {
            // Arrange
            var result = new StudentDisplayViewModel();
            var expect = "uri";

            //act
            result.EmotionImgUri = expect;

            // Assert
            Assert.AreEqual(expect, result.EmotionImgUri, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
