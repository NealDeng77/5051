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
    public class IdentityDataSourceTableUnitTests
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
        public void Backend_IdentityDataSourceTable_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceTable.Instance;

            //var expect = backend;

            // Act
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate

        #region Delete
        [TestMethod]
        public void Backend_IdentityDataSourceTable_Delete_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceTable.Instance;

            // Act
            var result = backend.DeleteUser(null);

            //Reset
            backend.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_Delete_Valid_ID_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceTable.Instance;
            var testDefault = backend.ListAllUsers().FirstOrDefault();
            var expectId = testDefault.Id;

            // Act
            var result = backend.DeleteUser(expectId);

            //Reset
            backend.Reset();

            // Assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_Delete_Invalid_ID_Not_Found_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var badID = "bogus";

            //act
            var result = backend.DeleteUser(badID);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        //[TestMethod]
        //public void Backend_IdentityDataSourceTabel_Delete_Student_User_Should_Pass()
        //{
        //    //arrange
        //    var backend = IdentityDataSourceTable.Instance;
        //    var student = backend.ListAllStudentUsers().FirstOrDefault();


        //    //act
        //    var result = backend.DeleteUser();

        //    //assert
        //    Assert.IsTrue(result, TestContext.TestName);
        //}
        #endregion

        #region ListUsers
        [TestMethod]
        public void Backend_IdentityDataSourceTable_ListAllSupportUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectNumSupportUsers = 1;

            //act
            var result = backend.ListAllSupportUsers();

            //assert
            Assert.AreEqual(expectNumSupportUsers, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_ListAllTeacherUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectNumTeacherUsers = 2;

            //act
            var result = backend.ListAllTeacherUsers();

            //assert
            Assert.AreEqual(expectNumTeacherUsers, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_ListAllStudentUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectNumStudents = 5;

            //act
            var result = backend.ListAllStudentUsers();

            //assert
            Assert.AreEqual(expectNumStudents, result.Count, TestContext.TestName);
        }
        #endregion

        #region LoadDataSet
        [TestMethod]
        public void Backend_IdentityDataSourceTable_LoadDataSet_Demo_Data_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.Demo;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_LoadDataSet_UnitTest_Data_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion

        #region FindUser
        [TestMethod]
        public void Backend_IdentityDataSourceTable_FindUserById_Null_ID_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;

            //act
            var result = backend.FindUserByID(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_FindUserByName_Null_Name_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;

            //act
            var result = backend.FindUserByUserName(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_FindUserByName_Valid_Name_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var name = backend.ListAllUsers().FirstOrDefault().UserName;

            //act
            var result = backend.FindUserByUserName(name);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_FindStudentById_Invalid_Should_Return_Null()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var badID = "bogus";

            //act
            var result = backend.GetStudentById(badID);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion

        #region Claims
        [TestMethod]
        public void Backend_IdentityDataSourceTable_UserHasClaimOfValue_Null_UserID_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;

            //act
            var result = backend.UserHasClaimOfValue(null, null, null);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }


        [TestMethod]
        public void Backend_IdentityDataSourceTable_AddClaimToUser_Null_UserID_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;

            //act
            var result = backend.AddClaimToUser(null, null, null);

            //Reset
            backend.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }


        [TestMethod]
        public void Backend_IdentityDataSourceTable_RemoveClaimFromUser_Null_UserID_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;

            //act
            var result = backend.RemoveClaimFromUser(null, null);

            //Reset
            backend.Reset();

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_RemoveClaimFromUser_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var testUserId = backend.ListAllSupportUsers().FirstOrDefault().Id;
            var testClaim = "testClaim";
            var addTestClaim = backend.AddClaimToUser(testUserId, testClaim, "test");

            //act
            var result = backend.RemoveClaimFromUser(testUserId, testClaim);

            //Reset
            backend.Reset();

            //assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_RemoveClaimFromUser_No_Claims_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var testId = backend.supportUserName;
            var firstClaimRemove = backend.RemoveClaimFromUser(testId, "SupportUser");
            var secondClaimRemove = backend.RemoveClaimFromUser(testId, "TeacherUser");

            //act
            var result = backend.RemoveClaimFromUser(testId, "");

            //reset
            backend.Reset();

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }
        #endregion

        #region Login
        [TestMethod]
        public void Backend_IdentityDataSourceTable_Login_Valid_User_Support_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectUsername = "su5051";

            //act
            var result = backend.LogUserIn(expectUsername, expectUsername, IdentityDataSourceTable.IdentityRole.Support);

            //assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_Login_Valid_User_Teacher_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectUsername = "teacher";

            //act
            var result = backend.LogUserIn(expectUsername, expectUsername, IdentityDataSourceTable.IdentityRole.Teacher);

            //assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        //[TestMethod]
        //public void Backend_IdentityDataSourceTable_Login_Valid_User_Student_Should_Pass()
        //{
        //    // Arrange
        //    var backend = IdentityDataSourceTable.Instance;
        //    var studentTableBackend = StudentDataSourceTable.Instance;
        //    var expectStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
        //    var expectId = "GoodID";
        //    var expectName = "Billy";
        //    var expectAvatarLevel = 7;
        //    var expectExperiencePoints = 100;
        //    var expectTokens = 100;
        //    var expectStatus = _5051.Models.StudentStatusEnum.Out;
        //    var expectPassword = "passWORD23!";
        //    var expectEmotionCurrent = _5051.Models.EmotionStatusEnum.Happy;
        //    var expectAvatarComposite = new AvatarCompositeModel();

        //    expectStudent.Id = expectId;
        //    expectStudent.Name = expectName;
        //    expectStudent.AvatarLevel = expectAvatarLevel;
        //    expectStudent.ExperiencePoints = expectExperiencePoints;
        //    expectStudent.Tokens = expectTokens;
        //    expectStudent.Status = expectStatus;
        //    expectStudent.Password = expectPassword;
        //    expectStudent.EmotionCurrent = expectEmotionCurrent;
        //    expectStudent.AvatarComposite = expectAvatarComposite;

        //    var resultStudentUpdate = studentTableBackend.Update(expectStudent);
        //    var resultIDUpdate = backend.CreateNewStudent(expectStudent);

        //    // Act
        //    var result = backend.LogUserIn(expectName, expectPassword, IdentityDataSourceTable.IdentityRole.Student);

        //    //reset
        //    backend.Reset();
        //    studentTableBackend.Reset();
        //    studentBackend.Reset();

        //    // Assert
        //    Assert.IsTrue(result, TestContext.TestName);
        //}

        [TestMethod]
        public void Backend_IdentityDataSourceTable_Login_Null_User_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;

            //act
            var result = backend.LogUserIn(null, null, IdentityDataSourceTable.IdentityRole.Support);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_Login_Invalid_Password_Support_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectUsername = "su5051";
            var badPassword = "bogus";

            //act
            var result = backend.LogUserIn(expectUsername, badPassword, IdentityDataSourceTable.IdentityRole.Support);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_Login_Invalid_Password_Teacher_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectUsername = "teacher";
            var badPassword = "bogus";

            //act
            var result = backend.LogUserIn(expectUsername, badPassword, IdentityDataSourceTable.IdentityRole.Teacher);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_Login_Invalid_Password_Student_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectUsername = DataSourceBackend.Instance.StudentBackend.GetDefault().Name;
            var badPassword = "bogus";

            //act
            var result = backend.LogUserIn(expectUsername, badPassword, IdentityDataSourceTable.IdentityRole.Student);


            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_Login_Invalid_Support_Role_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectUsername = "teacher";

            //act
            var result = backend.LogUserIn(expectUsername, expectUsername, IdentityDataSourceTable.IdentityRole.Support);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_Login_Invalid_Teacher_Role_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectUsername = "Mike";

            //act
            var result = backend.LogUserIn(expectUsername, expectUsername, IdentityDataSourceTable.IdentityRole.Teacher);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_Login_Invalid_Username_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var badName = "bogus";

            //act
            var result = backend.LogUserIn(badName, badName, IdentityDataSourceTable.IdentityRole.Student);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_LogOut_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;

            //act
            var result = backend.LogUserOut();

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        //[TestMethod]
        //public void Backend_IdentityDataSourceTable_Login_Invalid_Student_Should_Fail()
        //{
        //    //arrange
        //    var backend = IdentityDataSourceTable.Instance;
        //    var expectName = DataSourceBackend.Instance.StudentBackend.GetDefault().Name;

        //    //act
        //    var result = backend.LogUserIn(expectName, expectName, IdentityDataSourceTable.IdentityRole.Student);

        //    //assert
        //    Assert.IsFalse(result, TestContext.TestName);
        //}
        #endregion

        #region ChangePassword
        [TestMethod]
        public void Backend_IdentityDataSourceTable_ChangePassword_Valid_Student_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
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
        public void Backend_IdentityDataSourceTable_ChangePassword_Valid_Teacher_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
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
        public void Backend_IdentityDataSourceTable_ChangePassword_Valid_Support_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
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
        public void Backend_IdentityDataSourceTable_ChangePassword_Invalid_User_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
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

        #region ChangeUserName
        [TestMethod]
        public void Backend_IdentityDataSourceTable_ChangeUserName_Invalid_Null_New_Name_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
            var expectId = backend.ListAllSupportUsers().FirstOrDefault().Id;

            //act
            var result = backend.ChangeUserName(expectId, null);

            //reset
            backend.Reset();

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceTable_ChangeUserName_Invalid_User_Id_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceTable.Instance;
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
    }
}
