using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class FactoryInventoryBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        #region delete
        [TestMethod]
        public void Backend_FactoryInventoryBackend_Delete_Valid_Data_Should_Pass()
        {
            //arrange
            var test = Backend.FactoryInventoryBackend.Instance;
            var data = new FactoryInventoryModel();
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
        public void Models_FactoryInventoryBackend_Delete_With_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = Backend.FactoryInventoryBackend.Instance;
            var expect = false;

            //act
            var result = test.Delete(null);

            //reset
            test.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region GetFactoryInventoryListItem
        [TestMethod]
        public void Backend_FactoryInventoryBackend_GetFactoryInventoryListItem_ID_Null_Should_Pass()
        {
            //arrange
            var test = Backend.FactoryInventoryBackend.Instance;

            //act
            var result = test.GetFactoryInventoryListItem(null);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion GetFactoryInventoryListItem

        #region GetFactoryInventoryUri
        [TestMethod]
        public void Backend_FactoryInventoryBackend_GetFactoryInventoryUri_Valid_Data_Should_Pass()
        {
            //arrange
            var test = Backend.FactoryInventoryBackend.Instance;
            var testID = test.GetFirstFactoryInventoryId();

            //act
            var result = test.GetFactoryInventoryUri(testID);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend_GetFactoryInventoryUri_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = Backend.FactoryInventoryBackend.Instance;

            //act
            var result = test.GetFactoryInventoryUri(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend_GetFactoryInventoryUri_Invalid_ID_Should_Fail()
        {
            //arrange
            var data = new FactoryInventoryModel();
            var test = Backend.FactoryInventoryBackend.Instance;
            data.Id = "bogus";

            //act
            var result = test.GetFactoryInventoryUri(data.Id);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion GetFactoryInventoryUri

        #region update
        [TestMethod]
        public void Backend_FactoryInventoryBackend_Update_Valid_Data_Should_Pass()
        {
            //arrange
            var test = Backend.FactoryInventoryBackend.Instance;

            var data = new FactoryInventoryModel();
            var createResult = test.Create(data);

            data.Name = "GoodTestName";
            data.Description = "Good Test Description";
            data.Uri = "GoodTestUri";
            data.Tokens = 100;
            data.Category = FactoryInventoryCategoryEnum.Music;

            var expect = data;

            //act
            var updateResult = test.Update(data);

            var result = test.Read(data.Id);

            //reset
            test.Reset();

            //assert
            Assert.IsNotNull(result, "Updated "+TestContext.TestName);
            Assert.AreEqual(expect.Name, result.Name, "Name "+TestContext.TestName);
            Assert.AreEqual(expect.Description, result.Description, "Description "+TestContext.TestName);
            Assert.AreEqual(expect.Uri, result.Uri, "URI "+TestContext.TestName);
            Assert.AreEqual(expect.Tokens, result.Tokens, "Tokens "+TestContext.TestName);
            Assert.AreEqual(expect.Category, result.Category, "Category " + TestContext.TestName);
        }

        [TestMethod]
        public void Models_FactoryInventoryBackend_Update_With_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = Backend.FactoryInventoryBackend.Instance;

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
        public void Backend_FactoryInventoryBackend_Index_Valid_Should_Pass()
        {
            //arrange
            var test = Backend.FactoryInventoryBackend.Instance;

            //act
            var result = test.Index();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion index

        #region read
        [TestMethod]
        public void Backend_FactoryInventoryBackend_Read_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = Backend.FactoryInventoryBackend.Instance;

            //act
            var result = test.Read(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend_Read_Valid_ID_Should_Pass()
        {
            //arrange
            var test = Backend.FactoryInventoryBackend.Instance;
            var testID = test.GetFirstFactoryInventoryId();

            //act
            var result = test.Read(testID);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read

        #region create
        [TestMethod]
        public void Backend_FactoryInventoryBackend_Create_Valid_Data_Should_Pass()
        {
            //arrange
            var test = Backend.FactoryInventoryBackend.Instance;
            var data = new FactoryInventoryModel();

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
        public void Backend_FactoryInventoryBackend_SetDataSourceDataSet_Uses_MockData_Should_Pass()
        {
            //arrange
            var test = Backend.FactoryInventoryBackend.Instance;
            var testDataSourceBackend = Backend.DataSourceBackend.Instance;
            var mockEnum = DataSourceDataSetEnum.Demo;

            //act
            testDataSourceBackend.SetDataSourceDataSet(mockEnum);

            //reset
            test.Reset();

            //assert
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend_SetDataSourceDatSet_Uses_SQLData_Should_Pass()
        {
            //arange
            var test = Backend.FactoryInventoryBackend.Instance;
            var testDataSourceBackend = Backend.DataSourceBackend.Instance;
            var SQLEnum = DataSourceEnum.SQL;

            //act
            testDataSourceBackend.SetDataSource(SQLEnum);

            //reset
            test.Reset();

            //asset
        }
        #endregion SetDataSourceDataSet

        #region GetDefault
        [TestMethod]
        public void Backend_FactoryInventoryBackend_GetDefault_Valid_Should_Pass()
        {
            //arrange
            var test = Backend.FactoryInventoryBackend.Instance;

            //act
            var result = test.GetDefault(FactoryInventoryCategoryEnum.Truck);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion GetDefault
    }
}
