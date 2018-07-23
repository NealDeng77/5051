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
    public class GameDataSourceMockUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_GameDataSourceMock_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = GameDataSourceMock.Instance;

            //var expect = backend;

            // Act
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion Instantiate

        #region delete
        [TestMethod]
        public void Models_GameDataSourceMock_Delete_With_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var backend = GameDataSourceMock.Instance;
            var expect = false;

            //act
            var result = backend.Delete(null);

            //reset
            backend.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_GameDataSourceMock_Delete_With_Valid_ID_Should_Pass()
        {
            //arrange
            var backend = GameDataSourceMock.Instance;
            var testModel = new GameModel();
            var createResult = backend.Create(testModel);
            var expect = true;

            //act
            var result = backend.Delete(testModel.Id);

            //reset
            backend.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region update
        [TestMethod]
        public void Models_GameDataSourceMock_Update_With_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var backend = GameDataSourceMock.Instance;

            //act
            var result = backend.Update(null);

            //reset
            backend.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_GameDataSourceMock_Update_With_Invalid_Data_GameID_Should_Fail()
        {
            //arrange
            var backend = GameDataSourceMock.Instance;
            var test = new GameModel();
            var expected = "bogus";
            test.Id = expected;

            //act
            var result = backend.Update(test);

            //reset
            backend.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_GameDataSourceMock_Update_With_Valid_Data_Should_Pass()
        {
            //arrange
            var backend = GameDataSourceMock.Instance;
            var data = new GameModel();
            var createResult = backend.Create(data);
            var expectId = "AGoodId";
            var expectName = "A good Name";
            var expectDescription = "A good descritpion";
            var expectUri = "A good uri";
            var expectLevel = 7;

            //act
            data.Id = expectId;
            data.Name = expectName;
            data.Description = expectDescription;
            data.Uri = expectUri;
            data.Level = expectLevel;

            var updateResult = backend.Update(data);

            var idResult = data.Id;
            var nameResult = data.Name;
            var descriptionResult = data.Description;
            var uriResult = data.Uri;
            var levelResult = data.Level;

            //reset
            backend.Reset();

            //assert
            Assert.IsNotNull(updateResult, TestContext.TestName);
            Assert.AreEqual(expectId, idResult, TestContext.TestName);
            Assert.AreEqual(expectName, nameResult, TestContext.TestName);
            Assert.AreEqual(expectDescription, descriptionResult, TestContext.TestName);
            Assert.AreEqual(expectUri, uriResult, TestContext.TestName);
            Assert.AreEqual(expectLevel, levelResult, TestContext.TestName);
        }
        #endregion update

        #region read
        [TestMethod]
        public void Models_GameDataSourceMock_Read_With_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var backend = GameDataSourceMock.Instance;

            //act
            var result = backend.Read(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_GameDataSourceMock_Read_With_Valid_ID_Should_Pass()
        {
            //arrange
            var backend = GameDataSourceMock.Instance;
            var testGameModel = GameBackend.Instance;
            var testID = testGameModel.GetFirstGameId();

            //act
            var result = backend.Read(testID);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read

        #region DataSet
        [TestMethod]
        public void Models_GameDataSourceMock_DataSet_Demo_Data_Should_Pass()
        {
            //arrange
            var backend = GameDataSourceMock.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.Demo;

            //act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //reset
            backend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_GameDataSourceMock_DataSet_UnitTest_Data_Should_Pass()
        {
            //arrange
            var backend = GameDataSourceMock.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            //act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //reset
            backend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion DataSet
    }
}
