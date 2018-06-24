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

        #endregion Instantiate
    }
}
