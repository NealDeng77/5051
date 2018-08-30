using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;
using Microsoft.WindowsAzure.Storage.Table;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class DataSourceBackendTableUnitTests
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
        public void Backend_DataSourceBackendTable_Default_Instantiate_Should_Pass()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            // Set for ServerTest
            DataSourceBackend.Instance.SetDataSource(DataSourceEnum.ServerTest);

            var backend = DataSourceBackend.Instance;

            //var expect = backend;

            // Act
            var result = backend.AvatarItemBackend;

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion Instantiate

        #region Load
        [TestMethod]
        public void Backend_DataSourceBackendTable_Load_Invalid_Table_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Load<AvatarItemModel>(null, pk,rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Load_Invalid_pk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Load<AvatarItemModel>(table,null, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Load_Invalid_rk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Load<AvatarItemModel>(table, pk, null);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Load_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Load<AvatarItemModel>(table, pk, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        #endregion Load


        #region LoadDirect
        [TestMethod]
        public void Backend_DataSourceBackendTable_LoadDirect_Invalid_Table_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.LoadDirect<AvatarItemModel>(null, pk, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_LoadDirect_Invalid_pk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.LoadDirect<AvatarItemModel>(table, null, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_LoadDirect_Invalid_rk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.LoadDirect<AvatarItemModel>(table, pk, null);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_LoadDirect_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.LoadDirect<AvatarItemModel>(table, pk, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        #endregion LoadDirect


        #region Update
        [TestMethod]
        public void Backend_DataSourceBackendTable_Update_Invalid_Table_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Update<AvatarItemModel>(null, pk, rk,data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Update_Invalid_pk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Update<AvatarItemModel>(table, null, rk,data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Update_Invalid_rk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Update<AvatarItemModel>(table, pk, null,data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Update_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Update<AvatarItemModel>(table, pk, rk,data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        #endregion Update


        #region UpdateDirect
        [TestMethod]
        public void Backend_DataSourceBackendTable_UpdateDirect_Invalid_Table_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.UpdateDirect<AvatarItemModel>(null, pk, rk,data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_UpdateDirect_Invalid_pk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.UpdateDirect<AvatarItemModel>(table, null, rk,data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_UpdateDirect_Invalid_rk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.UpdateDirect<AvatarItemModel>(table, pk, null,data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_UpdateDirect_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.UpdateDirect<AvatarItemModel>(table, pk, rk, data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        #endregion UpdateDirect
        //[TestMethod]
        //public void Backend_DataSourceBackendTable_Load_Valid_ID_Should_Pass()
        //{
        //    // Arrange
        //    DataSourceBackend.SetTestingMode(true);

        //    var backend = DataSourceBackendTable.Instance;
        //    var shopBackend = AvatarItemBackend.Instance;
        //    var expectShopItem = shopBackend.GetFirstAvatarItemId();

        //    // Act
        //    var result = backend.Load(expectShopItem);

        //    // Reset
        //    DataSourceBackend.Instance.Reset();
        //    DataSourceBackend.SetTestingMode(false);

        //    // Assert
        //    Assert.IsNotNull(result, TestContext.TestName);
        //}
        //#endregion Load

        //#region update
        //[TestMethod]
        //public void Backend_DataSourceBackendTable_Update_Invalid_Data_Null_Should_Fail()
        //{
        //    // Arrange
        //    DataSourceBackend.SetTestingMode(true);

        //    var backend = DataSourceBackendTable.Instance;

        //    // Act
        //    var result = backend.Update(null);

        //    // Reset
        //    DataSourceBackend.Instance.Reset();
        //    DataSourceBackend.SetTestingMode(false);

        //    // Assert
        //    Assert.IsNull(result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Backend_DataSourceBackendTable_Update_Invalid_Data_Shop_Item_Does_Not_Exist_Should_Fail()
        //{
        //    // Arrange
        //    DataSourceBackend.SetTestingMode(true);

        //    var backend = DataSourceBackendTable.Instance;
        //    var expectShopModel = new AvatarItemModel();

        //    // Act
        //    var result = backend.Update(expectShopModel);

        //    // Reset
        //    DataSourceBackend.Instance.Reset();
        //    DataSourceBackend.SetTestingMode(false);

        //    // Assert
        //    Assert.IsNull(result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Backend_DataSourceBackendTable_Update_Valid_Data_Should_Pass()
        //{
        //    // Arrange
        //    DataSourceBackend.SetTestingMode(true);

        //    var backend = DataSourceBackendTable.Instance;
        //    var shopBackend = AvatarItemBackend.Instance;
        //    var expectShopItemId = shopBackend.GetFirstAvatarItemId();
        //    var expectShopModel = backend.Load(expectShopItemId);
        //    var expectId = "GoodID";
        //    var expectUri = "GoodUri";
        //    var expectName = "Shop Item";
        //    var expectDescription = "A good description of the item";
        //    var expectCatagory = AvatarItemCategoryEnum.Accessory;
        //    var expectTokens = 1000;

        //    // Act
        //    expectShopModel.Id = expectId;
        //    expectShopModel.Uri = expectUri;
        //    expectShopModel.Name = expectName;
        //    expectShopModel.Description = expectDescription;
        //    expectShopModel.Category = expectCatagory;
        //    expectShopModel.Tokens = expectTokens;

        //    var resultUpdate = backend.Update(expectShopModel);

        //    // Reset
        //    DataSourceBackend.Instance.Reset();
        //    DataSourceBackend.SetTestingMode(false);

        //    // Assert
        //    Assert.AreEqual(expectShopModel, resultUpdate, TestContext.TestName);
        //    Assert.AreEqual(expectId, resultUpdate.Id, TestContext.TestName);
        //    Assert.AreEqual(expectUri, resultUpdate.Uri, TestContext.TestName);
        //    Assert.AreEqual(expectName, resultUpdate.Name, TestContext.TestName);
        //    Assert.AreEqual(expectDescription, resultUpdate.Description, TestContext.TestName);
        //    Assert.AreEqual(expectCatagory, resultUpdate.Category, TestContext.TestName);
        //    Assert.AreEqual(expectTokens, resultUpdate.Tokens, TestContext.TestName);
        //}
        //#endregion update

        //#region delete
        //[TestMethod]
        //public void Backend_DataSourceBackendTable_Delete_Invalid_ID_Null_Should_Fail()
        //{
        //    // Arrange
        //    DataSourceBackend.SetTestingMode(true);

        //    var backend = DataSourceBackendTable.Instance;
        //    var expect = false;

        //    // Act
        //    var result = backend.Delete(null);

        //    // Reset
        //    DataSourceBackend.Instance.Reset();
        //    DataSourceBackend.SetTestingMode(false);

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Backend_DataSourceBackendTable_Delete_Invalid_ID_Bogus_Should_Fail()
        //{
        //    // Arrange
        //    DataSourceBackend.SetTestingMode(true);

        //    var backend = DataSourceBackendTable.Instance;
        //    var expect = false;

        //    // Act
        //    var result = backend.Delete("bogus");

        //    // Reset
        //    DataSourceBackend.Instance.Reset();
        //    DataSourceBackend.SetTestingMode(false);

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Backend_DataSourceBackendTable_Delete_Valid_ID_Should_Pass()
        //{
        //    // Arrange
        //    DataSourceBackend.SetTestingMode(true);

        //    var backend = DataSourceBackendTable.Instance;

        //    DataSourceBackend.Instance.SetDataSource(DataSourceEnum.Local);
        //    var shopBackend = DataSourceBackend.Instance.AvatarItemBackend;

        //    var expectShopItem = shopBackend.GetFirstAvatarItemId();
        //    var expect = true;

        //    // Act
        //    var result = backend.Delete(expectShopItem);

        //    // Reset
        //    DataSourceBackend.Instance.Reset();
        //    DataSourceBackend.SetTestingMode(false);

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}
        //#endregion delete

        //#region DataSet
        //[TestMethod]
        //public void Backend_DataSourceBackendTable_LoadDataSet_Valid_Enum_Demo_Should_Pass()
        //{
        //    // Arrange
        //    DataSourceBackend.SetTestingMode(true);

        //    var backend = DataSourceBackendTable.Instance;
        //    var expectEnum = _5051.Models.DataSourceDataSetEnum.Demo;

        //    // Act
        //    backend.LoadDataSet(expectEnum);
        //    var result = backend;

        //    // Reset
        //    DataSourceBackend.Instance.Reset();
        //    DataSourceBackend.SetTestingMode(false);

        //    // Assert
        //    Assert.IsNotNull(result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Backend_DataSourceBackendTable_LoadDataSet_Valid_Enum_UnitTest_Should_Pass()
        //{
        //    // Arrange
        //    DataSourceBackend.SetTestingMode(true);

        //    var backend = DataSourceBackendTable.Instance;
        //    var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

        //    // Act
        //    backend.LoadDataSet(expectEnum);
        //    var result = backend;

        //    // Reset
        //    DataSourceBackend.Instance.Reset();
        //    DataSourceBackend.SetTestingMode(false);

        //    // Assert
        //    Assert.IsNotNull(result, TestContext.TestName);
        //}
        //#endregion DataSet
    }
}
