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
    public class IdentityDataSourceMockUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Backend_IdentityDataSourceMock_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;

            // Act
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion

        #region ChangeUserName
        [TestMethod]
        public void Backend_IdentityDataSourceMock_ChangeUserName_Valid_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectId = backend.ListAllStudentUsers().FirstOrDefault().Id;
            var expectNewName = "testName";

            //act
            var result = backend.ChangeUserName(expectId, expectNewName);
            var resultName = backend.FindUserByID(expectId).UserName;
            var resultStudentName = DataSourceBackend.Instance.StudentBackend.Read(expectId).Name;

            //reset
            DataSourceBackend.Instance.Reset();
            backend.Reset();

            //assert
            Assert.IsTrue(result, TestContext.TestName);
            Assert.AreEqual(expectNewName, resultName, TestContext.TestName);
            Assert.AreEqual(expectNewName, resultStudentName, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ChangeUserName_Invalid_Null_New_Name_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectId = backend.ListAllSupportUsers().FirstOrDefault().Id;

            //act
            var result = backend.ChangeUserName(expectId, null);

            //reset
            backend.Reset();

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ChangeUserName_Invalid_User_Id_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectId = "badID";
            var expectNewName = "testName";

            //act
           var result = backend.ChangeUserName(expectId, expectNewName);

            //reset
            backend.Reset();

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }
        #endregion

        #region ChangePassword
        //right now this test isn't reseting properly
        [TestMethod]
        public void Backend_IdentityDataSourceMock_ChangePassword_Valid_Student_Should_Pass()
        {
            //arrange
            var expectStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var backend = IdentityDataSourceMockV2.Instance;
            var expectName = expectStudent.Name;
            var expectNewPass = "goodPassword";

            //act
            var result = backend.ChangeUserPassword(expectName, expectNewPass, IdentityDataSourceTable.IdentityRole.Student);
            var passwordResult = expectStudent.Password;

            //Reset
            DataSourceBackend.Instance.StudentBackend.Reset();
            backend.Reset();

            //assert
            Assert.IsTrue(result, TestContext.TestName);
            Assert.AreEqual(expectNewPass, passwordResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ChangePassword_Valid_Teacher_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectName = backend.teacherUserName;
            var expectNewPass = "goodPassword";

            //act
            var result = backend.ChangeUserPassword(expectName, expectNewPass, IdentityDataSourceTable.IdentityRole.Teacher);
            var passwordResult = backend.teacherPass;

            //Reset
            backend.Reset();

            //assert
            Assert.IsTrue(result, TestContext.TestName);
            Assert.AreEqual(expectNewPass, passwordResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ChangePassword_Valid_Support_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectName = backend.supportUserName;
            var expectNewPass = "goodPassword";

            //act
            var result = backend.ChangeUserPassword(expectName, expectNewPass, IdentityDataSourceTable.IdentityRole.Support);
            var passwordResult = backend.supportPass;

            //Reset
            backend.Reset();

            //assert
            Assert.IsTrue(result, TestContext.TestName);
            Assert.AreEqual(expectNewPass, passwordResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ChangePassword_Invalid_User_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectName = "badName";
            var expectNewPass = "goodPassword";

            //act
            var result = backend.ChangeUserPassword(expectName, expectNewPass, IdentityDataSourceTable.IdentityRole.Support);
            var passwordResult = backend.supportPass;

            //Reset
            backend.Reset();

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }
        #endregion

        #region Claims
        [TestMethod]
        public void Backend_IdentityDataSourceMock_UserHasClaimOfValue_Invalid_User_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;

            //act
            var result = backend.UserHasClaimOfValue(null, null, null);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_AddClaimToUser_Invalid_User_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;

            //act
            var result = backend.AddClaimToUser(null, null, null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_RemoveClaimFromUser_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectId = "su5051";
            var expectClaimType = "test";
            var expectClaimValue = "True";
            var addClaimResult = backend.AddClaimToUser(expectId, expectClaimType, expectClaimValue);
            var expectNumClaimsBefore = backend.FindUserByID(expectId).Claims.Count;

            //act
            var result = backend.RemoveClaimFromUser(expectId, expectClaimType);
            var resultClaimNum = backend.FindUserByID(expectId).Claims.Count;

            //reset
            backend.Reset();

            //assert
            Assert.IsTrue(result, TestContext.TestName);
            Assert.AreEqual(expectNumClaimsBefore-1, resultClaimNum, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_RemoveClaimFromUser_Invalid_user_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;

            //act
            var result = backend.RemoveClaimFromUser(null, null);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_RemoveClaimFromUser_Invalid_Claim_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectId = "su5051";
            var expectInvalidClaim = "test";

            //act
            var result = backend.RemoveClaimFromUser(expectId, expectInvalidClaim);

            //reset
            backend.Reset();

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }
        #endregion

        #region Create
        [TestMethod]
        public void Backend_IdentityDataSourceMock_CreateNewStudent_SHould_Pass()
        {
            //arrange
            var expectName = "testName";
            var backend = IdentityDataSourceMockV2.Instance;
            var testStudent = new StudentModel();
            testStudent.Name = expectName;
            var studentCountBefore = backend.ListAllStudentUsers().Count;

            //act
            var result = backend.CreateNewStudent(testStudent);
            var studentCountAfter = backend.ListAllStudentUsers().Count;

            //reset
            DataSourceBackend.Instance.Reset();
            backend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
            Assert.AreEqual(expectName, result.Name, TestContext.TestName);
        }
        #endregion

        #region Delete
        [TestMethod]
        public void Backend_IdentityDataSourceMock_DeleteUser_Valid_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserId = backend.ListAllStudentUsers().FirstOrDefault().Id;
            var numUsersBefore = backend.ListAllUsers().Count;

            // Act
            var result = backend.DeleteUser(expectUserId);
            var numUsersAfter = backend.ListAllUsers().Count;

            //reset
            DataSourceBackend.Instance.Reset();
            backend.Reset();

            // Assert
            Assert.IsTrue(result, TestContext.TestName);
            Assert.AreEqual(numUsersBefore-1, numUsersAfter, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_DeleteUser_Null_ID_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;

            // Act
            var result = backend.DeleteUser(null);

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_DeleteUser_Id_Invalid_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserId = "bogus";

            // Act
            var result = backend.DeleteUser(expectUserId);

            //reset
            backend.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }
        #endregion

        #region FindUser
        [TestMethod]
        public void Backend_IdentityDataSourceMock_FindByUserName_Valid_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "Mike";

            //act
            var result = backend.FindUserByUserName(expectUserName);

            //assert
            Assert.AreEqual(expectUserName, result.UserName, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_FindByUserName_Null_Name_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            string expectUserName = null;

            //act
            var result = backend.FindUserByUserName(expectUserName);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_FindByUserId_Null_ID_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            string expectId = null;

            //act
            var result = backend.FindUserByID(expectId);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_GetStudentById_Invalid_Id_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;

            // Act
            var result = backend.GetStudentById(null);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion

        #region LoadDataSet
        [TestMethod]
        public void Backend_IdentityDataSourceMock_LoadDataSet_Demo_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectEnum = DataSourceDataSetEnum.Demo;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LoadDataSet_UnitTest_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion

        #region ListUsers
        [TestMethod]
        public void Backend_IdentityDataSourceMock_ListAllUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectNumUsers = 7;

            //act
            var result = backend.ListAllUsers();

            //assert
            Assert.AreEqual(expectNumUsers, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ListAllSupportUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectNumUsers = 1;

            //act
            var result = backend.ListAllSupportUsers();

            //assert
            Assert.AreEqual(expectNumUsers, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ListAllTeacherUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectNumUsers = 2;

            //act
            var result = backend.ListAllTeacherUsers();

            //assert
            Assert.AreEqual(expectNumUsers, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ListAllStudentUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectNumUsers = 5;

            //act
            var result = backend.ListAllStudentUsers();

            //assert
            Assert.AreEqual(expectNumUsers, result.Count, TestContext.TestName);
        }
        #endregion

        #region Login
        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Valid_Support_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "su5051";
            var expectPassword = "su5051";

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, IdentityDataSourceTable.IdentityRole.Support);

            //reset
            backend.Reset();

            // Assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Valid_Teacher_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "teacher";
            var expectPassword = "teacher";

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, IdentityDataSourceTable.IdentityRole.Teacher);

            //reset
            backend.Reset();

            // Assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Valid_Student_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "Mike";
            var expectPassword = "Mike";

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, IdentityDataSourceTable.IdentityRole.Student);

            //reset
            backend.Reset();

            // Assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Invalid_Null_Username_Password_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;

            // Act
            var result = backend.LogUserIn(null, null, IdentityDataSourceTable.IdentityRole.Support);

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserInIn_Invalid_UserName_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectPassword = "test";

            // Act
            var result = backend.LogUserIn(null, expectPassword, IdentityDataSourceTable.IdentityRole.Support);

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Invalid_Support_Bad_Password_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "su5051";
            var expectPassword = "badpassword";

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, IdentityDataSourceTable.IdentityRole.Support);

            //reset
            backend.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Invalid_Support_User_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "teacher";
            var expectPassword = "teacher";

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, IdentityDataSourceTable.IdentityRole.Support);

            //reset
            backend.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Invalid_Teacher_User_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "Mike";
            var expectPassword = "Mike";

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, IdentityDataSourceTable.IdentityRole.Teacher);

            //reset
            backend.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Invalid_Teacher_Password_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "teacher";
            var expectPassword = "badpassword";

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, IdentityDataSourceTable.IdentityRole.Teacher);

            //reset
            backend.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Invalid_Student_Password_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "Mike";
            var expectPassword = "badpassword";

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, IdentityDataSourceTable.IdentityRole.Student);

            //reset
            backend.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_Logout_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;

            // Act
            var result = backend.LogUserOut();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }
        #endregion


    }
}
