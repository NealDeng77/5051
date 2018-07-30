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
    public class StudentBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Read
        [TestMethod]
        public void Backend_StudentBackend_Read_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var backend = StudentBackend.Instance;

            //act
            var result = backend.Read(null);

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
            var backend = StudentBackend.Instance;
            var expectStudent = backend.GetDefault();

            //act
            var result = backend.Read(expectStudent.Id);

            //assert
            Assert.AreEqual(expectStudent ,result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Read_Valid_ID_Has_Attendance_Should_Pass()
        {
            //arrange
            var backend = StudentBackend.Instance;
            var expectStudent = backend.GetDefault();
            var tempAttendance = new AttendanceModel();
            expectStudent.Attendance.Add(tempAttendance);

            //act
            var result = backend.Read(expectStudent.Id);

            //reset
            backend.Reset();

            //assert
            Assert.AreEqual(expectStudent, result, TestContext.TestName);
        }
        #endregion Read

        #region Update
        [TestMethod]
        public void Backend_StudentBackend_Update_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var backend = StudentBackend.Instance;

            //act
            var result = backend.Update(null);

            //reset
            backend.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Update_Invalid_Data_Does_Not_Exist_Should_Fail()
        {
            //arrange
            var backend = StudentBackend.Instance;
            var testStudent = new StudentModel();

            //act
            var result = backend.Update(testStudent);

            //reset
            backend.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Update_Valid_Data_Status_Change_Should_Pass()
        {
            //arrange
            var backend = StudentBackend.Instance;
            var statusAfterEnum = _5051.Models.StudentStatusEnum.In;
            var testStudent = new StudentModel();
            testStudent.Status = statusAfterEnum;
            testStudent.Id = backend.GetDefault().Id;

            //act
            var updateResult = backend.Update(testStudent);

            //reset
            backend.Reset();

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
            var backend = StudentBackend.Instance;
            var expect = false;

            //act
            var result = backend.Delete(null);

            //reset
            backend.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Delete_Valid_ID_Should_Pass()
        {
            //arrange
            var backend = StudentBackend.Instance;
            var testStudent = new StudentModel();
            var createResult = backend.Create(testStudent);
            var expect = true;

            //act
            var deleteResult = backend.Delete(createResult.Id);

            //reset
            backend.Reset();

            //assert
            Assert.AreEqual(expect, deleteResult, TestContext.TestName);
        }
        #endregion

        #region Index
        [TestMethod]
        public void Backend_StudentBackend_Index_Valid_Should_Pass()
        {
            //arrange
            var backend = StudentBackend.Instance;

            //act
            var result = backend.Index();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion

        #region ToggleStatus
        [TestMethod]
        public void Backend_StudentBackend_ToggleStatusById_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var backend = StudentBackend.Instance;

            //act
            backend.ToggleStatusById(null);
            var result = backend;

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
            var backend = StudentBackend.Instance;
            var testStudent = backend.GetDefault();
            var expectToggleResult = _5051.Models.StudentStatusEnum.Out;
            testStudent.Status = _5051.Models.StudentStatusEnum.In;
            var updateResult = backend.Update(testStudent);

            //act
            backend.ToggleStatusById(testStudent.Id);
            var toggleResult = testStudent.Status;

            //reset
            backend.Reset();

            //assert
            Assert.AreEqual(expectToggleResult, toggleResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleEmotionStatusById_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var backend = StudentBackend.Instance;
            var emotion = EmotionStatusEnum.Neutral;

            //act
            backend.ToggleEmotionStatusById(null, emotion);
            var result = backend;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleEmotionStatusById_Invalid_ID_Does_Not_Exist_Should_Fail()
        {
            //arrange
            var backend = StudentBackend.Instance;
            var testStudent = new StudentModel();
            var emotion = EmotionStatusEnum.Neutral;

            //act
            backend.ToggleEmotionStatusById(testStudent.Id, emotion);
            var result = backend;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleEmotionStatusById_Valid_Id_Should_Pass()
        {
            //arrange
            var backend = StudentBackend.Instance;
            var testStudent = backend.GetDefault();
            testStudent.Status = StudentStatusEnum.Out;
            var updateResult = backend.Update(testStudent);
            var emotion = EmotionStatusEnum.Neutral;

            var expectToggleStatus = StudentStatusEnum.In;
            var expectToggleEmotion = EmotionStatusEnum.Neutral;
                        
            //act
            backend.ToggleEmotionStatusById(testStudent.Id, emotion);
            var toggleStatus = testStudent.Status;
            var toggleEmotion = testStudent.EmotionCurrent;

            //reset
            backend.Reset();

            //assert
            Assert.AreEqual(expectToggleStatus, toggleStatus, TestContext.TestName);
            Assert.AreEqual(expectToggleEmotion, toggleEmotion, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleStatus_Valid_Id_Case_Out_Should_Pass()
        {
            //arrange
            var backend = StudentBackend.Instance;
            var testStudent = backend.GetDefault();
            var expectToggleResult = _5051.Models.StudentStatusEnum.In;
            testStudent.Status = _5051.Models.StudentStatusEnum.Out;
            var updateResult = backend.Update(testStudent);

            //act
            backend.ToggleStatus(testStudent);
            var toggleResult = testStudent.Status;

            //reset
            backend.Reset();

            //assert
            Assert.AreEqual(expectToggleResult, toggleResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleStatus_Valid_Id_Case_Hold_Should_Pass()
        {
            //arrange
            var backend = StudentBackend.Instance;
            var testStudent = backend.GetDefault();
            var expectToggleResult = _5051.Models.StudentStatusEnum.Out;
            testStudent.Status = _5051.Models.StudentStatusEnum.Hold;
            var updateResult = backend.Update(testStudent);

            //act
            backend.ToggleStatus(testStudent);
            var toggleResult = testStudent.Status;

            //reset
            backend.Reset();

            //assert
            Assert.AreEqual(expectToggleResult, toggleResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleStatus_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var backend = StudentBackend.Instance;

            //act
            backend.ToggleStatus(null);
            var result = backend;

            //reset
            backend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ToggleStatus

        #region SetLogin / Logout
        [TestMethod]
        public void Backend_StudentBackend_SetLogIn_Invalid_Id_Null_Should_Fail()
        {
            //arrange
            var backend = StudentBackend.Instance;

            //act
            backend.SetLogIn(null);
            var result = backend;

            //reset
            backend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_SetLogOut_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var backend = StudentBackend.Instance;

            //act
            backend.SetLogOut(null);
            var result = backend;

            //reset
            backend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_SetLogOut_Valid_Data_Has_Attendance_Should_Pass()
        {
            //arrange
            var backend = StudentBackend.Instance;
            var testStudent = backend.GetDefault();
            var tempAttendance = new AttendanceModel();
            testStudent.Attendance.Add(tempAttendance);

            //act
            backend.SetLogOut(testStudent);
            var result = backend;

            //reset
            backend.Reset();

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
            var backend = StudentBackend.Instance;

            //act
            StudentBackend.SetDataSource(sqlEnum);
            var result = backend;

            //reset
            backend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_SetDataSourceDataSet_Valid_Enum_UnitTests_Should_Pass()
        {
            //arrange
            var unitEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;
            var backend = StudentBackend.Instance;

            //act
            StudentBackend.SetDataSourceDataSet(unitEnum);
            var result = backend;

            //reset
            backend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion
    }
}
