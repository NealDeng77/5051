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
    public class GameBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        #region delete
        [TestMethod]
        public void Backend_GameBackend_Delete_Valid_Data_Should_Pass()
        {
            //arrange
            var test = Backend.GameBackend.Instance;
            var data = new GameModel();
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
        public void Models_GameBackend_Delete_With_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = Backend.GameBackend.Instance;
            var expect = false;

            //act
            var result = test.Delete(null);

            //reset
            test.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region update
        [TestMethod]
        public void Backend_GameBackend_Update_Valid_Data_Should_Pass()
        {
            //arrange
            var expectRunDate = DateTime.Parse("01/23/2018");
            var expectEnabled = true;
            var expectIterationNumber = 123;

            var test = Backend.GameBackend.Instance;
            var data = new GameModel();

            test.Create(data);

            data.RunDate = expectRunDate;
            data.Enabled = expectEnabled;
            data.IterationNumber = expectIterationNumber;

            //act
            var updateResult = test.Update(data);
            var result = test.Read(data.Id);

            //reset
            test.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
            Assert.AreEqual(expectRunDate, result.RunDate, "Run Date "+ TestContext.TestName);
            Assert.AreEqual(expectEnabled, result.Enabled, "Enabled " + TestContext.TestName);
            Assert.AreEqual(expectIterationNumber, result.IterationNumber, "Iteration Number "+TestContext.TestName);
        }

        [TestMethod]
        public void Models_GameBackend_Update_With_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = Backend.GameBackend.Instance;

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
        public void Backend_GameBackend_Index_Valid_Should_Pass()
        {
            //arrange
            var test = Backend.GameBackend.Instance;

            //act
            var result = test.Index();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion index

        #region read
        [TestMethod]
        public void Backend_GameBackend_Read_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = Backend.GameBackend.Instance;

            //act
            var result = test.Read(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_Read_Valid_ID_Should_Pass()
        {
            //arrange
            var test = Backend.GameBackend.Instance;
            var testID = test.GetFirstGameId();

            //act
            var result = test.Read(testID);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read

        #region create
        [TestMethod]
        public void Backend_GameBackend_Create_Valid_Data_Should_Pass()
        {
            //arrange
            var test = Backend.GameBackend.Instance;
            var data = new GameModel();

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
        public void Backend_GameBackend_SetDataSourceDataSet_Uses_MockData_Should_Pass()
        {
            //arrange
            var test = Backend.GameBackend.Instance;
            var testDataSourceBackend = Backend.DataSourceBackend.Instance;
            var mockEnum = DataSourceDataSetEnum.Demo;

            //act
            testDataSourceBackend.SetDataSourceDataSet(mockEnum);

            //reset
            test.Reset();

            //assert
        }

        [TestMethod]
        public void Backend_GameBackend_SetDataSourceDatSet_Uses_SQLData_Should_Pass()
        {
            //arange
            var test = Backend.GameBackend.Instance;
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
