using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class ShopInventoryDataSourceMockUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_ShopInventoryDataSourceMock_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = ShopInventoryDataSourceMock.Instance;

            //var expect = backend;

            // Act
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion Instantiate

        #region read
        [TestMethod]
        public void Backend_ShopInventoryDataSourceMock_Read_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = ShopInventoryDataSourceMock.Instance;

            // Act
            var result = backend.Read(null);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_ShopInventoryDataSourceMock_Read_Valid_ID_Should_Pass()
        {
            // Arrange
            var backend = ShopInventoryDataSourceMock.Instance;
            var shopBackend = ShopInventoryBackend.Instance;
            var expectShopItem = shopBackend.GetFirstShopInventoryId();

            // Act
            var result = backend.Read(expectShopItem);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read

        #region update
        [TestMethod]
        public void Backend_ShopInventoryDataSourceMock_Update_Invalid_Data_Null_Should_Fail()
        {
            // Arrange
            var backend = ShopInventoryDataSourceMock.Instance;

            // Act
            var result = backend.Update(null);

            //reset
            backend.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_ShopInventoryDataSourceMock_Update_Invalid_Data_Shop_Item_Does_Not_Exist_Should_Fail()
        {
            // Arrange
            var backend = ShopInventoryDataSourceMock.Instance;
            var expectShopModel = new ShopInventoryModel();

            // Act
            var result = backend.Update(expectShopModel);

            //reset
            backend.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_ShopInventoryDataSourceMock_Update_Valid_Data_Should_Pass()
        {
            // Arrange
            var backend = ShopInventoryDataSourceMock.Instance;
            var shopBackend = ShopInventoryBackend.Instance;
            var expectShopItemId = shopBackend.GetFirstShopInventoryId();
            var expectShopModel = backend.Read(expectShopItemId);
            var expectId = "GoodID";
            var expectUri = "GoodUri";
            var expectName = "Shop Item";
            var expectDescription = "A good description of the item";
            var expectCatagory = _5051.Models.ShopInventoryCategoryEnum.Entertainment;
            var expectTokens = 1000;

            // Act
            expectShopModel.Id = expectId;
            expectShopModel.Uri = expectUri;
            expectShopModel.Name = expectName;
            expectShopModel.Description = expectDescription;
            expectShopModel.Category = expectCatagory;
            expectShopModel.Tokens = expectTokens;

            var resultUpdate = backend.Update(expectShopModel);

            //reset
            backend.Reset();
            shopBackend.Reset();

            // Assert
            Assert.AreEqual(expectShopModel, resultUpdate, TestContext.TestName);
            Assert.AreEqual(expectId, resultUpdate.Id, TestContext.TestName);
            Assert.AreEqual(expectUri, resultUpdate.Uri, TestContext.TestName);
            Assert.AreEqual(expectName, resultUpdate.Name, TestContext.TestName);
            Assert.AreEqual(expectDescription, resultUpdate.Description, TestContext.TestName);
            Assert.AreEqual(expectCatagory, resultUpdate.Category, TestContext.TestName);
            Assert.AreEqual(expectTokens, resultUpdate.Tokens, TestContext.TestName);
        }
        #endregion update

        #region delete
        [TestMethod]
        public void Backend_ShopInventoryDataSourceMock_Delete_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = ShopInventoryDataSourceMock.Instance;
            var expect = false;

            // Act
            var result = backend.Delete(null);

            //reset
            backend.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_ShopInventoryDataSourceMock_Delete_Valid_ID_Should_Pass()
        {
            // Arrange
            var backend = ShopInventoryDataSourceMock.Instance;
            var shopBackend = ShopInventoryBackend.Instance;
            var expectShopItem = shopBackend.GetFirstShopInventoryId();
            var expect = true;

            // Act
            var result = backend.Delete(expectShopItem);

            //reset
            backend.Reset();
            shopBackend.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region DataSet
        [TestMethod]
        public void Backend_ShopInventoryDataSourceMock_LoadDataSet_Valid_Enum_Demo_Should_Pass()
        {
            // Arrange
            var backend = ShopInventoryDataSourceMock.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.Demo;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_ShopInventoryDataSourceMock_LoadDataSet_Valid_Enum_UnitTest_Should_Pass()
        {
            // Arrange
            var backend = ShopInventoryDataSourceMock.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion DataSet
    }
}
