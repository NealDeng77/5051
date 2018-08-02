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
            var testID = test.GetDefault();

            //act
            var result = test.Read(testID.Id);

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

        #region GetResults

        [TestMethod]
        public void Backend_GameBackend_GetResults_Valid_ID_Should_Pass()
        {
            //arrange
            var test = Backend.GameBackend.Instance;

            //act
            var result = test.GetResult("test");

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion GetResults

        #region Simulation
        [TestMethod]
        public void Backend_GameBackend_Simulation_Valid_ID_Should_Pass()
        {
            //arrange
            var test = Backend.GameBackend.Instance;
            var data = test.GetDefault();

            //act
            var result = test.Simulation();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_Simulation_RunDate_UTCNow_Should_Skip()
        {
            //arrange
            var test = Backend.GameBackend.Instance;
            var data = test.GetDefault();
            data.RunDate = DateTime.UtcNow;

            //act
            var result = test.Simulation();

            // Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_Simulation_RunDate_UTCNow_Add_1_Should_Skip()
        {
            //arrange
            var test = Backend.GameBackend.Instance;
            var data = test.GetDefault();
            data.RunDate = DateTime.UtcNow.AddTicks(1);
            test.Update(data);

            var expect = test.GetDefault().IterationNumber;

            //act
            var result = test.Simulation();

            // Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_Simulation_RunDate_UTCNow_Minus_1_Should_Skip()
        {
            //arrange
            var test = Backend.GameBackend.Instance;
            var data = test.GetDefault();
            data.RunDate = DateTime.UtcNow.AddMinutes(-10); // Move it back 10 minutes in time
            test.Update(data);

            var expect = test.GetDefault().IterationNumber;

            //act
            var result = test.Simulation();

            // Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreNotEqual(expect, result, TestContext.TestName);
        }

        #endregion Simulation

        #region RunIteration
        [TestMethod]
        public void Backend_GameBackend_RunIteration_Student_Is_Null_should_Pass()
        {
            // arrange
            var test = Backend.GameBackend.Instance;

            // act
            test.RunIteration();

            // assert
            
        }

        [TestMethod]
        public void Backend_GameBackend_RunIteration_Student_Is_Not_Null_should_Pass()
        {
            // arrange
            var test = Backend.GameBackend.Instance;
            var data1 = new StudentModel();
            var data2 = new StudentModel();
            var student1 = Backend.StudentBackend.Instance.Create(data1);
            var student2 = Backend.StudentBackend.Instance.Create(data2);

            // Reset StudentBackend
            StudentBackend.Instance.Reset();

            // act
            test.RunIteration();
            
            // assert
        }
        #endregion RunIteration

        #region CalculateStudentIteration
        [TestMethod]
        public void Backend_GameBackend_CalculateStudentIteration_should_Pass()
        {
            // arrange
            var test = Backend.GameBackend.Instance;
            var data = new StudentModel();
            var student = Backend.StudentBackend.Instance.Create(data);


            // act         

            test.CalculateStudentIteration(student);          

            // Reset StudentBackend
            StudentBackend.Instance.Reset();

            // assert         
        }

        #endregion CalculateStudentIteration

        #region PayRentPerDay
        [TestMethod]
        public void Backend_GameBackend_PayRentPerDay_Valid_ID_Should_Pass()
        {
            //arrange
            var test = Backend.GameBackend.Instance;
            var data = test.GetDefault();
            var studentData = new StudentModel();
            var student = Backend.StudentBackend.Instance.Create(studentData);

            //act
            test.PayRentPerDay(student);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
        }

        [TestMethod]
        public void Backend_GameBackend_PayRentPerDay_RunDate_UTCNow_Should_Skip()
        {
            //arrange
            var test = Backend.GameBackend.Instance;
            var data = test.GetDefault();
            data.RunDate = DateTime.UtcNow;
            var studentData = new StudentModel();
            var student = Backend.StudentBackend.Instance.Create(studentData);

            //act
            test.PayRentPerDay(student);

            // Reset
            DataSourceBackend.Instance.Reset();

            //assert
        }

        [TestMethod]
        public void Backend_GameBackend_PayRentPerDay_RunDate_UTCNow_Add_1_Should_Skip()
        {
            //arrange          
            var test = Backend.GameBackend.Instance;
            var data = test.GetDefault();
            data.RunDate = DateTime.UtcNow.AddHours(1);
            test.Update(data);
            var studentData = new StudentModel();
            var student = Backend.StudentBackend.Instance.Create(studentData);

            //act   
            test.PayRentPerDay(student);
            var myTokens = student.Tokens;

            DataSourceBackend.Instance.StudentBackend.Update(student);

            var expect = student.Tokens;

            // Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expect, myTokens, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_PayRentPerDay_RunDate_UTCNow_Minus_25_Should_Pass()
        {
            //arrange          
            var test = Backend.GameBackend.Instance;
            var data = test.GetDefault();
            data.RunDate = DateTime.UtcNow.AddHours(-25);
            test.Update(data);
            var studentData = new StudentModel();
            var student = Backend.StudentBackend.Instance.Create(studentData);

            //act   
            var expect = student.Tokens - 1;
            test.PayRentPerDay(student);
            
            DataSourceBackend.Instance.StudentBackend.Update(student);

            var myTokens = student.Tokens;

            // Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expect, myTokens, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_PayRentPerDay_StudentTokens_Less_than_1_Should_Skip()
        {
            //arrange          
            var test = Backend.GameBackend.Instance;
            var data = test.GetDefault();
            data.RunDate = DateTime.UtcNow.AddHours(-25);
            test.Update(data);
            var studentData = new StudentModel();
            var student = Backend.StudentBackend.Instance.Create(studentData);
            student.Tokens = 0;
            //act   
            var expect = student.Tokens;
            test.PayRentPerDay(student);

            DataSourceBackend.Instance.StudentBackend.Update(student);

            var myTokens = student.Tokens;

            // Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expect, myTokens, TestContext.TestName);
        }

        #endregion PayRentPerDay
    }


}
