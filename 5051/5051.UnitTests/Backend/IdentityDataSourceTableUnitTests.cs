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
    public class IdentityDataSourceTableUnitTests
    {
        public TestContext TestContext { get; set; }
        
        [TestInitialize()]
        public void Initialize()
        {
            DataSourceBackend.SetTestingMode(true);
            DataSourceBackend.Instance.SetDataSource(DataSourceEnum.Local);
        }

        [TestCleanup]
        public void Cleanup()
        {
            DataSourceBackend.SetTestingMode(false);
            DataSourceBackend.Instance.SetDataSource(DataSourceEnum.Mock);
        }


        #region Instantiate
        [TestMethod]
        public void Backend_IdentityDataSourceTable_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceTable.Instance;

            //var expect = backend;

            // Act
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate

        #region Delete
        [TestMethod]
        public void Backend_IdentityDataSourceTable_Delete_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceTable.Instance;

            // Act
            var result = backend.DeleteUser(null);

            //Reset
            backend.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_Delete_Valid_ID_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceTable.Instance;
            var testDefault = backend.ListAllUsers().FirstOrDefault();
            var expectId = testDefault.Id;

            // Act
            var result = backend.DeleteUser(expectId);

            //Reset
            backend.Reset();

            // Assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        #endregion

        #region ListUsers
        [TestMethod]
        public void Backend_IdentityDataSourceTable_ListAllSupportUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectNumSupportUsers = 1;

            //act
            var result = backend.ListAllSupportUsers();

            //assert
            Assert.AreEqual(expectNumSupportUsers, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_ListAllTeacherUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectNumTeacherUsers = 2;

            //act
            var result = backend.ListAllTeacherUsers();

            //assert
            Assert.AreEqual(expectNumTeacherUsers, result.Count, TestContext.TestName);
        }
        #endregion

        #region LoadDataSet
        [TestMethod]
        public void Backend_IdentityDataSourceTable_LoadDataSet_Demo_Data_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.Demo;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_LoadDataSet_UnitTest_Data_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion

        #region FindUser
        [TestMethod]
        public void Backend_IdentityDataSourceTable_FindUserById_Null_ID_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;

            //act
            var result = backend.FindUserByID(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_FindUserByName_Null_Name_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;

            //act
            var result = backend.FindUserByUserName(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_FindUserByName_Valid_Name_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var name = backend.ListAllUsers().FirstOrDefault().UserName;

            //act
            var result = backend.FindUserByUserName(name);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion
    }
}
