using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class ShopInventoryBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        #region delete
        [TestMethod]
        public void Backend_ShopInventoryBackend_Delete_Valid_Data_Should_Pass()
        {
            //arrange
            var test = Backend.ShopInventoryBackend.Instance;
            var data = new ShopInventoryModel();
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
        public void Models_ShopInventoryBackend_Delete_With_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = Backend.ShopInventoryBackend.Instance;
            var expect = false;

            //act
            var result = test.Delete(null);

            //reset
            test.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region GetShopInventoryListItem
        [TestMethod]
        public void Backend_ShopInventoryBackend_GetShopInventoryListItem_ID_Null_Should_Pass()
        {
            //arrange
            var test = Backend.ShopInventoryBackend.Instance;

            //act
            var result = test.GetShopInventoryListItem(null);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion GetShopInventoryListItem

        #region GetShopInventoryUri
        [TestMethod]
        public void Backend_ShopInventoryBackend_GetShopInventoryUri_Valid_Data_Should_Pass()
        {
            //arrange
            var test = Backend.ShopInventoryBackend.Instance;
            var testID = test.GetFirstShopInventoryId();

            //act
            var result = test.GetShopInventoryUri(testID);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_ShopInventoryBackend_GetShopInventoryUri_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = Backend.ShopInventoryBackend.Instance;

            //act
            var result = test.GetShopInventoryUri(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_ShopInventoryBackend_GetShopInventoryUri_Invalid_ID_Should_Fail()
        {
            //arrange
            var data = new ShopInventoryModel();
            var test = Backend.ShopInventoryBackend.Instance;
            data.Id = "bogus";

            //act
            var result = test.GetShopInventoryUri(data.Id);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion GetShopInventoryUri

        #region update
        [TestMethod]
        public void Backend_ShopInventoryBackend_Update_Valid_Data_Should_Pass()
        {
            //arrange
            var test = Backend.ShopInventoryBackend.Instance;

            var data = new ShopInventoryModel();
            var createResult = test.Create(data);

            data.Name = "GoodTestName";
            data.Description = "Good Test Description";
            data.Uri = "GoodTestUri";
            data.Tokens = 100;
            data.Category = ShopInventoryCategoryEnum.Music;

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
        public void Models_ShopInventoryBackend_Update_With_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = Backend.ShopInventoryBackend.Instance;

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
        public void Backend_ShopInventoryBackend_Index_Valid_Should_Pass()
        {
            //arrange
            var test = Backend.ShopInventoryBackend.Instance;

            //act
            var result = test.Index();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion index

        #region read
        [TestMethod]
        public void Backend_ShopInventoryBackend_Read_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = Backend.ShopInventoryBackend.Instance;

            //act
            var result = test.Read(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_ShopInventoryBackend_Read_Valid_ID_Should_Pass()
        {
            //arrange
            var test = Backend.ShopInventoryBackend.Instance;
            var testID = test.GetFirstShopInventoryId();

            //act
            var result = test.Read(testID);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read

        #region create
        [TestMethod]
        public void Backend_ShopInventoryBackend_Create_Valid_Data_Should_Pass()
        {
            //arrange
            var test = Backend.ShopInventoryBackend.Instance;
            var data = new ShopInventoryModel();

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
        public void Backend_ShopInventoryBackend_SetDataSourceDataSet_Uses_MockData_Should_Pass()
        {
            //arrange
            var test = Backend.ShopInventoryBackend.Instance;
            var testDataSourceBackend = Backend.DataSourceBackend.Instance;
            var mockEnum = DataSourceDataSetEnum.Demo;

            //act
            testDataSourceBackend.SetDataSourceDataSet(mockEnum);

            //reset
            test.Reset();

            //assert
        }

        [TestMethod]
        public void Backend_ShopInventoryBackend_SetDataSourceDatSet_Uses_SQLData_Should_Pass()
        {
            //arange
            var test = Backend.ShopInventoryBackend.Instance;
            var testDataSourceBackend = Backend.DataSourceBackend.Instance;
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
