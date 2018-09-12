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
    public class IdentityDataSourceMockUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Backend_IdentityDataSourceMock_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;

            // Act
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion

        #region FindUser
        [TestMethod]
        public void Backend_IdentityDataSourceMock_FindByUserName_Valid_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "Mike";

            //act
            var result = backend.FindUserByUserName(expectUserName);

            //assert
            Assert.AreEqual(expectUserName, result.UserName, TestContext.TestName);
        }

        //[TestMethod]
        //public void Backend_IdentityDataSourceMock_FindByUserName_Null_Name_Should_Fail()
        //{
        //    //arrange
        //    var backend = IdentityDataSourceMockV2.Instance;
        //    string expectUserName = null;

        //    //act
        //    var result = backend.FindUserByUserName(expectUserName);

        //    //assert
        //    Assert.IsNull(result.UserName, TestContext.TestName);
        //}

        [TestMethod]
        public void Backend_IdentityDataSourceMock_FindByUserId_Null_ID_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            string expectId = null;

            //act
            var result = backend.FindUserByID(expectId);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion

        #region LoadDataSet
        [TestMethod]
        public void Backend_IdentityDataSourceMock_LoadDataSet_Demo_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectEnum = DataSourceDataSetEnum.Demo;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LoadDataSet_UnitTest_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion

        #region ListUsers
        [TestMethod]
        public void Backend_IdentityDataSourceMock_ListAllUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectNumUsers = 7;

            //act
            var result = backend.ListAllUsers();

            //assert
            Assert.AreEqual(expectNumUsers, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ListAllSupportUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectNumUsers = 1;

            //act
            var result = backend.ListAllSupportUsers();

            //assert
            Assert.AreEqual(expectNumUsers, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ListAllTeacherUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectNumUsers = 2;

            //act
            var result = backend.ListAllTeacherUsers();

            //assert
            Assert.AreEqual(expectNumUsers, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ListAllStudentUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectNumUsers = 5;

            //act
            var result = backend.ListAllStudentUsers();

            //assert
            Assert.AreEqual(expectNumUsers, result.Count, TestContext.TestName);
        }
        #endregion

        #region Login






        [TestMethod]
        public void Backend_IdentityDataSourceMock_Logout_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;

            // Act
            var result = backend.LogUserOut();

            //reset
            backend.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }
        #endregion
    }
}
