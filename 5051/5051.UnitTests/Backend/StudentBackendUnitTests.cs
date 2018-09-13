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
    public class StudentBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Read
        [TestMethod]
        public void Backend_StudentBackend_Read_Invalid_ID_Null_Should_Fail()
        {
            //arrange

            //act
            var result = DataSourceBackend.Instance.StudentBackend.Read(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Read_Invalid_ID_Does_Not_Exist_Should_Fail()
        {
            //arrange
            var backend = StudentBackend.Instance;
            var testStudent = new StudentModel();

            //act
            var result = backend.Read(testStudent.Id);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Read_Valid_ID_No_Attendance_Should_Pass()
        {
            //arrange
            var expectStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();

            //act
            var result = DataSourceBackend.Instance.StudentBackend.Read(expectStudent.Id);

            //assert
            Assert.AreEqual(expectStudent ,result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Read_Valid_ID_Has_Attendance_Should_Pass()
        {
            //arrange
            var expectStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var tempAttendance = new AttendanceModel();
            expectStudent.Attendance.Add(tempAttendance);

            //act
            var result = DataSourceBackend.Instance.StudentBackend.Read(expectStudent.Id);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expectStudent, result, TestContext.TestName);
        }
        #endregion Read

        #region Update
        [TestMethod]
        public void Backend_StudentBackend_Update_Invalid_Data_Null_Should_Fail()
        {
            //arrange

            //act
            var result = DataSourceBackend.Instance.StudentBackend.Update(null);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Update_Invalid_Data_Does_Not_Exist_Should_Fail()
        {
            //arrange
            var testStudent = new StudentModel();

            //act
            var result = DataSourceBackend.Instance.StudentBackend.Update(testStudent);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Update_Valid_Data_Status_Change_Should_Pass()
        {
            //arrange
            var statusAfterEnum = _5051.Models.StudentStatusEnum.In;
            var testStudent = new StudentModel();
            testStudent.Status = statusAfterEnum;
            testStudent.Id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            //act
            var updateResult = DataSourceBackend.Instance.StudentBackend.Update(testStudent);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(updateResult, TestContext.TestName);
            Assert.AreEqual(statusAfterEnum, updateResult.Status, TestContext.TestName);
        }
        #endregion

        #region Delete
        [TestMethod]
        public void Backend_StudentBackend_Delete_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var expect = false;

            //act
            var result = DataSourceBackend.Instance.StudentBackend.Delete(null);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Delete_Valid_ID_Should_Pass()
        {
            //arrange
            var testStudent = new StudentModel();
            var createResult = DataSourceBackend.Instance.StudentBackend.Create(testStudent);
            var expect = true;

            //act
            var deleteResult = DataSourceBackend.Instance.StudentBackend.Delete(createResult.Id);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expect, deleteResult, TestContext.TestName);
        }
        #endregion

        #region Index
        [TestMethod]
        public void Backend_StudentBackend_Index_Valid_Should_Pass()
        {
            //arrange

            //act
            var result = DataSourceBackend.Instance.StudentBackend.Index();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion

        #region ToggleStatus
        [TestMethod]
        public void Backend_StudentBackend_ToggleStatusById_Invalid_ID_Null_Should_Fail()
        {
            //arrange

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleStatusById(null);
            var result = DataSourceBackend.Instance.StudentBackend;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleStatusById_Invalid_ID_Does_Not_Exist_Should_Fail()
        {
            //arrange
            var backend = StudentBackend.Instance;
            var testStudent = new StudentModel();

            //act
            backend.ToggleStatusById(testStudent.Id);
            var result = backend;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleStatusById_Valid_Id_Case_In_Should_Pass()
        {
            //arrange
            var testStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expectToggleResult = _5051.Models.StudentStatusEnum.Out;
            testStudent.Status = _5051.Models.StudentStatusEnum.In;
            var updateResult = DataSourceBackend.Instance.StudentBackend.Update(testStudent);

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleStatusById(testStudent.Id);
            var toggleResult = testStudent.Status;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expectToggleResult, toggleResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleEmotionStatusById_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var emotion = EmotionStatusEnum.Neutral;

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleEmotionStatusById(null, emotion);
            var result = DataSourceBackend.Instance.StudentBackend;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleEmotionStatusById_Invalid_ID_Does_Not_Exist_Should_Fail()
        {
            //arrange
            var testStudent = new StudentModel();
            var emotion = EmotionStatusEnum.Neutral;

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleEmotionStatusById(testStudent.Id, emotion);
            var result = DataSourceBackend.Instance.StudentBackend;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleEmotionStatusById_Valid_Id_Should_Pass()
        {
            //arrange
            var testStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            testStudent.Status = StudentStatusEnum.Out;
            var updateResult = DataSourceBackend.Instance.StudentBackend.Update(testStudent);
            var emotion = EmotionStatusEnum.Neutral;

            var expectToggleStatus = StudentStatusEnum.In;
            var expectToggleEmotion = EmotionStatusEnum.Neutral;

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleEmotionStatusById(testStudent.Id, emotion);
            var toggleStatus = testStudent.Status;
            var toggleEmotion = testStudent.EmotionCurrent;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expectToggleStatus, toggleStatus, TestContext.TestName);
            Assert.AreEqual(expectToggleEmotion, toggleEmotion, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleStatus_Valid_Id_Case_Out_Should_Pass()
        {
            //arrange
            var testStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expectToggleResult = _5051.Models.StudentStatusEnum.In;
            testStudent.Status = _5051.Models.StudentStatusEnum.Out;
            var updateResult = DataSourceBackend.Instance.StudentBackend.Update(testStudent);

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleStatus(testStudent);
            var toggleResult = testStudent.Status;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expectToggleResult, toggleResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleStatus_Valid_Id_Case_Hold_Should_Pass()
        {
            //arrange
            var testStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expectToggleResult = _5051.Models.StudentStatusEnum.Out;
            testStudent.Status = _5051.Models.StudentStatusEnum.Hold;
            var updateResult = DataSourceBackend.Instance.StudentBackend.Update(testStudent);

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleStatus(testStudent);
            var toggleResult = testStudent.Status;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expectToggleResult, toggleResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleStatus_Invalid_ID_Null_Should_Fail()
        {
            //arrange

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleStatus(null);
            var result = DataSourceBackend.Instance.StudentBackend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ToggleStatus

        #region SetLogin / Logout
        [TestMethod]
        public void Backend_StudentBackend_SetLogIn_Invalid_Id_Null_Should_Fail()
        {
            //arrange

            //act
            DataSourceBackend.Instance.StudentBackend.SetLogIn(null);
            var result = DataSourceBackend.Instance.StudentBackend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_SetLogOut_Invalid_ID_Null_Should_Fail()
        {
            //arrange

            //act
            DataSourceBackend.Instance.StudentBackend.SetLogOut(null);
            var result = DataSourceBackend.Instance.StudentBackend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_SetLogOut_Valid_Data_Has_Attendance_Should_Pass()
        {
            //arrange
            var testStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var tempAttendance = new AttendanceModel();
            testStudent.Attendance.Add(tempAttendance);

            //act
            DataSourceBackend.Instance.StudentBackend.SetLogOut(testStudent);
            var result = DataSourceBackend.Instance.StudentBackend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion

        #region SetDataSource
        [TestMethod]
        public void Backend_StudentBackend_SetDataSource_Valid_Enum_SQL_Should_Pass()
        {
            //arrange
            var sqlEnum = _5051.Models.DataSourceEnum.SQL;

            //act
            DataSourceBackend.Instance.SetDataSource(sqlEnum);
            var result = DataSourceBackend.Instance.StudentBackend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_SetDataSourceDataSet_Valid_Enum_UnitTests_Should_Pass()
        {
            //arrange
            var unitEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            //act
            DataSourceBackend.Instance.SetDataSourceDataSet(unitEnum);
            var result = DataSourceBackend.Instance.StudentBackend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion
    }
}
