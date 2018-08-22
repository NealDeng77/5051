using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using _5051.Models;
using _5051.Controllers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Backend;
using System.Web.SessionState;
using System.IO;
using System.Reflection;
using Moq;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class IdentityBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        #region CreateNewSupportUser
        [TestMethod]
        public void Backend_IdentityBackend_CreateNewSupportUser_Valid_User_Should_Pass()
        {
            //arrange
            var expectedClaimCount = 2;
            var testUsername = "testsu5051";
            var dummyUser = new ApplicationUser() { UserName = testUsername, Email = testUsername + "@seattleu.edu", Id = testUsername };

            var claimIdentity1 = new IdentityUserClaim()
            {
                ClaimType = "SupportUser",
                ClaimValue = "True",
            };
            var claimIdentity2 = new IdentityUserClaim()
            {
                ClaimType = "TeacherUser",
                ClaimValue = "True",
            };

            dummyUser.Claims.Add(claimIdentity1);
            dummyUser.Claims.Add(claimIdentity2);

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUsername))
                .ReturnsAsync(dummyUser);
            var claimsManager = userStore.As<IUserClaimStore<ApplicationUser>>();

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);

            //act
            var result = backend.CreateNewSupportUser(testUsername, testUsername, testUsername);

            //assert
            Assert.AreEqual(testUsername, result.UserName, TestContext.TestName);
            Assert.AreEqual(expectedClaimCount, result.Claims.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityBackend_CreateNewSupportUser_Result_Failed_Should_Return_Null()
        {
            //arrange
            var testUsername = "testSupport";

            var dummyUser = new ApplicationUser() { UserName = testUsername, Email = testUsername + "@seattleu.edu", Id = testUsername };

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUsername))
                .ReturnsAsync(dummyUser);
            var claimsManager = userStore.As<IUserClaimStore<ApplicationUser>>();

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);


            //act
            //pass in a bad password, causes create failure
            var result = backend.CreateNewSupportUser(testUsername, "", testUsername);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion

        #region CreateNewTeacher
        [TestMethod]
        public void Backend_IdentityBackend_CreateNewTeacher_Valid_User_Should_Pass()
        {
            //arrange
            var expectedClaimCount = 2;
            var testUsername = "testTeacher";
            var dummyUser = new ApplicationUser() { UserName = testUsername, Email = testUsername + "@seattleu.edu", Id = testUsername };

            var claimIdentity1 = new IdentityUserClaim()
            {
                ClaimType = "TeacherUser",
                ClaimValue = "True",
            };
            var claimIdentity2 = new IdentityUserClaim()
            {
                ClaimType = "TeacherID",
                ClaimValue = testUsername,
            };

            dummyUser.Claims.Add(claimIdentity1);
            dummyUser.Claims.Add(claimIdentity2);

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUsername))
                .ReturnsAsync(dummyUser);
            var claimsManager = userStore.As<IUserClaimStore<ApplicationUser>>();

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);

            //act
            var result = backend.CreateNewTeacher(testUsername, testUsername, testUsername);

            //assert
            Assert.AreEqual(testUsername, result.UserName, TestContext.TestName);
            Assert.AreEqual(expectedClaimCount, result.Claims.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityBackend_CreateNewTeacher_Result_Failed_Should_Return_Null()
        {
            //arrange
            var testUsername = "testTeacher";

            var dummyUser = new ApplicationUser() { UserName = testUsername, Email = testUsername + "@seattleu.edu", Id = testUsername };

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUsername))
                .ReturnsAsync(dummyUser);
            var claimsManager = userStore.As<IUserClaimStore<ApplicationUser>>();

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);


            //act
            //pass in a bad password, causes create failure
            var result = backend.CreateNewTeacher(testUsername, "", testUsername);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion

        #region CreateNewStudent
        [TestMethod]
        public void Backend_IdentityBackend_CreateNewStudent_Valid_User_Should_Pass()
        {
            //arrange
            var testUsername = "testStudent";
            var expectedClaimCount = 2;
            var testStudent = new StudentModel();
            testStudent.Name = testUsername;
            testStudent.Id = testUsername;

            var dummyUser = new ApplicationUser() { UserName = testUsername, Email = testUsername + "@seattleu.edu", Id = testUsername };

            var claimIdentity1 = new IdentityUserClaim()
            {
                ClaimType = "StudentUser",
                ClaimValue = "True",
            };
            var claimIdentity2 = new IdentityUserClaim()
            {
                ClaimType = "StudentID",
                ClaimValue = testStudent.Id,
            };

            dummyUser.Claims.Add(claimIdentity1);
            dummyUser.Claims.Add(claimIdentity2);

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUsername))
                .ReturnsAsync(dummyUser);
            var claimsManager = userStore.As<IUserClaimStore<ApplicationUser>>();

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);

            //act
            var result = backend.CreateNewStudent(testStudent);

            //assert
            Assert.AreEqual(testUsername, result.UserName, TestContext.TestName);
            Assert.AreEqual(expectedClaimCount, result.Claims.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityBackend_CreateNewStudent_Result_Failed_Should_Return_Null()
        {
            //arrange
            var testUsername = "testStudent";
            var testStudent = new StudentModel();
            testStudent.Name = "";
            testStudent.Id = testUsername;


            var dummyUser = new ApplicationUser() { UserName = testUsername, Email = testUsername + "@seattleu.edu", Id = testUsername };

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUsername))
                .ReturnsAsync(dummyUser);
            var claimsManager = userStore.As<IUserClaimStore<ApplicationUser>>();

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);


            //act
            //pass in a bad password, causes create failure
            var result = backend.CreateNewStudent(testStudent);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion

        #region GetStudentByID
        [TestMethod]
        public void Backend_IdentityBackend_GetStudentById_Valid_Student_Should_Pass()
        {
            //arrange
            var backend = new IdentityBackend();
            var studentBackend = StudentBackend.Instance;
            var expectStudent = studentBackend.GetDefault();

            //act
            var result = backend.GetStudentById(expectStudent.Id);

            //assert
            Assert.AreEqual(expectStudent, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityBackend_GetStudentById_Invalid_Student_ID_Should_Fail()
        {
            //arrange
            var backend = new IdentityBackend();
            var fakeID = "bogus";

            //act
            var result = backend.GetStudentById(fakeID);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion

        #region FindUserByUserName
        [TestMethod]
        public void Backend_IdentityBackend_FindUserByUserName_Should_Pass()
        {
            //arrange
            var testUsername = "testUser";
            var dummyUser = new ApplicationUser() { UserName = testUsername, Email = testUsername + "@seattleu.edu", Id = testUsername };

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByNameAsync(testUsername))
                .ReturnsAsync(dummyUser);

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);

            //act
            var result = backend.FindUserByUserName(testUsername);

            //assert
            Assert.AreEqual(dummyUser, result, TestContext.TestName);
        }
        #endregion

        #region UpdateStudent
        [TestMethod]
        public void Backend_IdentityBackend_UpdateStudent_Null_Student_Should_Fail()
        {
            //arrange
            var backend = new IdentityBackend();

            //act
            var result = backend.UpdateStudent(null);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityBackend_UpdateStudent_ID_Not_Found_Should_Fail()
        {
            //arrange
            var testUsername = "testUser";
            var testUserID = "AGoodID";
            var dummyUser = new ApplicationUser() { UserName = testUsername, Email = testUsername + "@seattleu.edu", Id = testUserID };

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUserID))
                .ReturnsAsync(dummyUser);

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);


            var testStudent = new StudentModel();
            var badID = "bogus";
            testStudent.Id = badID;

            //act
            var result = backend.UpdateStudent(testStudent);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityBackend_UpdateStudent_Should_Pass()
        {
            //arrange
            var testUsername = "testUser";
            var testUserID = "AGoodID";
            var studentNewName = "testStudent";
            var dummyUser = new ApplicationUser() { UserName = testUsername, Email = testUsername + "@seattleu.edu", Id = testUserID };

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUserID))
                .ReturnsAsync(dummyUser);

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);


            var testStudent = new StudentModel();
            testStudent.Id = testUserID;
            testStudent.Name = studentNewName;

            //act
            var result = backend.UpdateStudent(testStudent);

            //assert
            Assert.IsTrue(result, TestContext.TestName);
            Assert.AreEqual(dummyUser.UserName, studentNewName, TestContext.TestName);

        }
        #endregion

        #region ListUsers
        [TestMethod]
        public void Backend_IdentityBackend_ListAllStudentUsers_Should_Pass()
        {
            //arrange
            var testUsername1 = "testUser1";
            var testUsername2 = "userNumber2";
            var expectStudentUsers = 1;
            var dummyUser1 = new ApplicationUser() { UserName = testUsername1, Email = testUsername1 + "@seattleu.edu", Id = testUsername1 };
            var dummyUser2 = new ApplicationUser() { UserName = testUsername2, Email = testUsername2 + "@seattleu.edu", Id = testUsername2 };

            var claimIdentity1 = new IdentityUserClaim()
            {
                ClaimType = "StudentUser",
                ClaimValue = "True",
            };
            var claimIdentity2 = new IdentityUserClaim()
            {
                ClaimType = "StudentUser",
                ClaimValue = "False",
            };

            //only 1 user is a support user
            dummyUser1.Claims.Add(claimIdentity1);
            dummyUser2.Claims.Add(claimIdentity2);

            var userList = new List<ApplicationUser>()
            {
                dummyUser1,
                dummyUser2,
            }.AsQueryable();

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser1))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager1 = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUsername1))
                .ReturnsAsync(dummyUser1);
            var passwordManager2 = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUsername2))
                .ReturnsAsync(dummyUser2);
            var claimsManager = userStore.As<IUserClaimStore<ApplicationUser>>();
            var iQueryManager = userStore.As<IQueryableUserStore<ApplicationUser>>()
                .Setup(x => x.Users)
                .Returns(userList);

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);

            //act
            var result = backend.ListAllStudentUsers();

            //assert
            Assert.AreEqual(expectStudentUsers, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityBackend_ListAllTeacherUsers_Should_Pass()
        {
            //arrange
            var testUsername1 = "testUser1";
            var testUsername2 = "userNumber2";
            var expectTeacherUsers = 1;
            var dummyUser1 = new ApplicationUser() { UserName = testUsername1, Email = testUsername1 + "@seattleu.edu", Id = testUsername1 };
            var dummyUser2 = new ApplicationUser() { UserName = testUsername2, Email = testUsername2 + "@seattleu.edu", Id = testUsername2 };

            var claimIdentity1 = new IdentityUserClaim()
            {
                ClaimType = "TeacherUser",
                ClaimValue = "True",
            };
            var claimIdentity2 = new IdentityUserClaim()
            {
                ClaimType = "TeacherUser",
                ClaimValue = "False",
            };

            //only 1 user is a support user
            dummyUser1.Claims.Add(claimIdentity1);
            dummyUser2.Claims.Add(claimIdentity2);

            var userList = new List<ApplicationUser>()
            {
                dummyUser1,
                dummyUser2,
            }.AsQueryable();

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser1))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager1 = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUsername1))
                .ReturnsAsync(dummyUser1);
            var passwordManager2 = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUsername2))
                .ReturnsAsync(dummyUser2);
            var claimsManager = userStore.As<IUserClaimStore<ApplicationUser>>();
            var iQueryManager = userStore.As<IQueryableUserStore<ApplicationUser>>()
                .Setup(x => x.Users)
                .Returns(userList);

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);

            //act
            var result = backend.ListAllTeacherUsers();

            //assert
            Assert.AreEqual(expectTeacherUsers, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityBackend_ListAllSupportUsers_Should_Pass()
        {
            //arrange
            var testUsername1 = "testUser1";
            var testUsername2 = "userNumber2";
            var expectSupportUsers = 1;
            var dummyUser1 = new ApplicationUser() { UserName = testUsername1, Email = testUsername1 + "@seattleu.edu", Id = testUsername1 };
            var dummyUser2 = new ApplicationUser() { UserName = testUsername2, Email = testUsername2 + "@seattleu.edu", Id = testUsername2 };

            var claimIdentity1 = new IdentityUserClaim()
            {
                ClaimType = "TeacherUser",
                ClaimValue = "True",
            };
            var claimIdentity2 = new IdentityUserClaim()
            {
                ClaimType = "SupportUser",
                ClaimValue = "True",
            };
            var claimIdentity3 = new IdentityUserClaim()
            {
                ClaimType = "SupportUser",
                ClaimValue = "False",
            };

            //only 1 user is a support user
            dummyUser1.Claims.Add(claimIdentity1);
            dummyUser1.Claims.Add(claimIdentity2);
            dummyUser2.Claims.Add(claimIdentity3);

            var userList = new List<ApplicationUser>()
            {
                dummyUser1,
                dummyUser2,
            }.AsQueryable();

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser1))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager1 = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUsername1))
                .ReturnsAsync(dummyUser1);
            var passwordManager2 = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUsername2))
                .ReturnsAsync(dummyUser2);
            var claimsManager = userStore.As<IUserClaimStore<ApplicationUser>>();
            var iQueryManager = userStore.As<IQueryableUserStore<ApplicationUser>>()
                .Setup(x => x.Users)
                .Returns(userList);

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);

            //act
            var result = backend.ListAllSupportUsers();

            //assert
            Assert.AreEqual(expectSupportUsers, result.Count, TestContext.TestName);
        }

        #endregion

        #region AddClaimToUser
        [TestMethod]
        public void Backend_IdentityBackend_AddClaimToUser_Failed_To_Find_User_Should_Fail()
        {
            //arrange
            var testUsername = "dummy";
            var testUserId = "AGoodID";
            var testClaimTypeToAdd = "testClaim";
            var testClaimValueToAdd = "value";

            var dummyUser = new ApplicationUser() { UserName = testUsername, Email = testUsername + "@seattleu.edu", Id = testUserId };

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>();
            //.Setup(x => x.FindByIdAsync(testUserId))
            //.ReturnsAsync(dummyUser);
            var claimsManager = userStore.As<IUserClaimStore<ApplicationUser>>();

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);

            //act
            var result = backend.AddClaimToUser(testUserId, testClaimTypeToAdd, testClaimValueToAdd);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        //[TestMethod]
        //public void Backend_IdentityBackend_AddClaimToUser_Failed_To_Add_Claim_Should_Fail()
        //{
        //    //arrange
        //    var testUsername = "dummy";
        //    var testUserId = "AGoodID";
        //    var testClaimTypeToAdd = "testClaim";
        //    var testClaimValueToAdd = "value";

        //    var dummyUser = new ApplicationUser() { UserName = testUsername, Email = testUsername + "@seattleu.edu", Id = testUserId };

        //    var userStore = new Mock<IUserStore<ApplicationUser>>();
        //    var createDummyUser = userStore
        //        .Setup(x => x.CreateAsync(dummyUser))
        //        .Returns(Task.FromResult(IdentityResult.Success));
        //    var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>()
        //        .Setup(x => x.FindByIdAsync(testUserId))
        //        .ReturnsAsync(dummyUser);
        //    var claimsManager = userStore.As<IUserClaimStore<ApplicationUser>>()
        //        .Setup(x => x.AddClaimAsync(dummyUser, new Claim(testClaimTypeToAdd, testClaimValueToAdd)))
        //        .Returns(Task.FromResult(IdentityResult.Failed("error", "should fail")));

        //    var userManager = new ApplicationUserManager(userStore.Object);

        //    var backend = new IdentityBackend(userManager, null);

        //    //act
        //    var result = backend.AddClaimToUser(testUserId, testClaimTypeToAdd, testClaimValueToAdd);

        //    //assert
        //    Assert.IsNull(result, TestContext.TestName);
        //}
        #endregion

        #region RemoveClaimFromUser
        [TestMethod]
        public void Backend_IdentityBackend_RemoveClaimFromUser_Should_Pass()
        {
            //arrange
            var testUsername = "testUser";
            var claimToRemove = "TeacherUser";
            IList<Claim> claimsList = new List<Claim>()
            {
                new Claim("TeacherUser", "True"),
                new Claim("SupportUser", "True"),
            };

            var dummyUser = new ApplicationUser() { UserName = testUsername, Email = testUsername + "@seattleu.edu", Id = testUsername };

            var claimIdentity1 = new IdentityUserClaim()
            {
                ClaimType = "TeacherUser",
                ClaimValue = "True",
            };
            var claimIdentity2 = new IdentityUserClaim()
            {
                ClaimType = "TeacherID",
                ClaimValue = testUsername,
            };

            dummyUser.Claims.Add(claimIdentity1);
            dummyUser.Claims.Add(claimIdentity2);

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUsername))
                .ReturnsAsync(dummyUser);
            var claimsManager = userStore.As<IUserClaimStore<ApplicationUser>>()
                .Setup(x => x.GetClaimsAsync(dummyUser))
                .Returns(Task.FromResult(claimsList));

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);

            //act
            var result = backend.RemoveClaimFromUser(testUsername, claimToRemove);

            //assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityBackend_RemoveClaimFromUser_Cannot_Find_Claims_Should_Fail()
        {
            //arrange
            var testUsername = "testUser";
            var claimToRemove = "TeacherUser";
            var dummyUser = new ApplicationUser() { UserName = testUsername, Email = testUsername + "@seattleu.edu", Id = testUsername };

            var claimIdentity1 = new IdentityUserClaim()
            {
                ClaimType = "TeacherUser",
                ClaimValue = "True",
            };
            var claimIdentity2 = new IdentityUserClaim()
            {
                ClaimType = "TeacherID",
                ClaimValue = testUsername,
            };

            dummyUser.Claims.Add(claimIdentity1);
            dummyUser.Claims.Add(claimIdentity2);

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var createDummyUser = userStore
                .Setup(x => x.CreateAsync(dummyUser))
                .Returns(Task.FromResult(IdentityResult.Success));
            var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>()
                .Setup(x => x.FindByIdAsync(testUsername))
                .ReturnsAsync(dummyUser);
            var claimsManager = userStore.As<IUserClaimStore<ApplicationUser>>();

            var userManager = new ApplicationUserManager(userStore.Object);

            var backend = new IdentityBackend(userManager, null);

            //act
            var result = backend.RemoveClaimFromUser(testUsername, claimToRemove);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        //ned a test for if removing the claim failed
        #endregion

        //[TestMethod]
        //public void Backend_IdentityBackend_GetIsTeacherUser_Should_Pass()
        //{
        //    //var mocks = new MockRepository();
        //    //IPrincipal mockPrincipal = mocks.CreateMock<IPrincipal>();
        //    //IIdentity mockIdentity = mocks.CreateMock<IIdentity>();
        //    //ApplicationContext.User = mockPrincipal;
        //    //using (mocks.Record())
        //    //{
        //    //    Expect.Call(mockPrincipal.IsInRole(Roles.ROLE_MAN_PERSON)).Return(true);
        //    //    Expect.Call(mockIdentity.Name).Return("ju");
        //    //    Expect.Call(mockPrincipal.Identity).Return(mockIdentity);
        //    //}
        //    var mockIdentity = new Mock<IIdentity>();

        //    //act
        //    var result = mockIdentity.Object.GetIsTeacherUser();

        //    //assert
        //    Assert.IsTrue(result, TestContext.TestName);
        //}
    }
}
