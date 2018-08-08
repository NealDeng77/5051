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
    public class AvatarBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        #region delete
        [TestMethod]
        public void Backend_AvatarBackend_Delete_Valid_Data_Should_Pass()
        {
            //arrange
            var test = AvatarBackend.Instance;
            var data = new AvatarModel();
            var createResult = test.Create(data);
            var expect = true;

            //act
            var deleteResult = test.Delete(data.Id);

            //reset
            test.Reset();

            //assert
            Assert.AreEqual(expect, deleteResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarBackend_Delete_With_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = AvatarBackend.Instance;
            var expect = false;

            //act
            var result = test.Delete(null);

            //reset
            test.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region GetAvatarListItem
        [TestMethod]
        public void Backend_AvatarBackend_GetAvatarListItem_ID_Null_Should_Pass()
        {
            //arrange
            var test = AvatarBackend.Instance;

            //act
            var result = test.GetAvatarListItem(null);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion GetAvatarListItem

        #region GetAvatarUri
        [TestMethod]
        public void Backend_AvatarBackend_GetAvatarUri_Valid_Data_Should_Pass()
        {
            //arrange
            var test = AvatarBackend.Instance;
            var testID = test.GetFirstAvatarId();

            //act
            var result = test.GetAvatarUri(testID);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarBackend_GetAvatarUri_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = AvatarBackend.Instance;

            //act
            var result = test.GetAvatarUri(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarBackend_GetAvatarUri_Invalid_ID_Should_Fail()
        {
            //arrange
            var data = new AvatarModel();
            var test = AvatarBackend.Instance;
            data.Id = "bogus";

            //act
            var result = test.GetAvatarUri(data.Id);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion GetAvatarUri

        #region update
        [TestMethod]
        public void Backend_AvatarBackend_Update_Valid_Data_Should_Pass()
        {
            //arrange
            var test = AvatarBackend.Instance;
            var data = new AvatarModel();
            var createResult = test.Create(data);
            var expectName = "GoodTestName";
            var expectDesc = "Good Test Description";
            var expectUri = "GoodTestUri";
            var expectLevel = 7;


            //act
            data.Name = expectName;
            data.Description = expectDesc;
            data.Level = expectLevel;
            data.Uri = expectUri;
            var updateResult = test.Update(data);
            var updateAfterRead = test.Read(data.Id);
            var nameResult = updateAfterRead.Name;
            var descResult = updateAfterRead.Description;
            var uriResult = updateAfterRead.Uri;
            var levelResult = updateAfterRead.Level;

            //reset
            test.Reset();

            //assert
            Assert.IsNotNull(updateResult, TestContext.TestName);
            Assert.AreEqual(expectName, nameResult, TestContext.TestName);
            Assert.AreEqual(expectDesc, descResult, TestContext.TestName);
            Assert.AreEqual(expectUri, uriResult, TestContext.TestName);
            Assert.AreEqual(expectLevel, levelResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarBackend_Update_With_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = AvatarBackend.Instance;

            //act
            var result = test.Update(null);

            //reset
            test.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion update

        #region index
        [TestMethod]
        public void Backend_AvatarBackend_Index_Valid_Should_Pass()
        {
            //arrange
            var test = AvatarBackend.Instance;

            //act
            var result = test.Index();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion index

        #region read
        [TestMethod]
        public void Backend_AvatarBackend_Read_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = AvatarBackend.Instance;

            //act
            var result = test.Read(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarBackend_Read_Valid_ID_Should_Pass()
        {
            //arrange
            var test = AvatarBackend.Instance;
            var testID = test.GetFirstAvatarId();

            //act
            var result = test.Read(testID);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read

        #region create
        [TestMethod]
        public void Backend_AvatarBackend_Create_Valid_Data_Should_Pass()
        {
            //arrange
            var test = AvatarBackend.Instance;
            var data = new AvatarModel();

            //act
            var result = test.Create(data);

            //reset
            test.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
            Assert.AreEqual(data.Id, result.Id, TestContext.TestName);
        }
        #endregion create

        #region SetDataSourceDataSet
        [TestMethod]
        public void Backend_AvatarBackend_SetDataSourceDataSet_Uses_MockData_Should_Pass()
        {
            //arrange
            var test = AvatarBackend.Instance;
            var testDataSourceBackend = DataSourceBackend.Instance;
            var mockEnum = DataSourceDataSetEnum.Demo;

            //act
            testDataSourceBackend.SetDataSourceDataSet(mockEnum);

            //reset
            test.Reset();

            //assert
        }

        [TestMethod]
        public void Backend_AvatarBackend_SetDataSourceDatSet_Uses_SQLData_Should_Pass()
        {
            //arange
            var test = AvatarBackend.Instance;
            var testDataSourceBackend = DataSourceBackend.Instance;
            var SQLEnum = DataSourceEnum.SQL;

            //act
            testDataSourceBackend.SetDataSource(SQLEnum);

            //reset
            test.Reset();

            //asset
        }
        #endregion SetDataSourceDataSet
    }
}
