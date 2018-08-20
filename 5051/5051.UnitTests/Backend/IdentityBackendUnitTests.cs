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

        [TestMethod]
        public void Backend_IdentityBackend_CreateSupportUser_Valid_User_Should_Pass()
        {
            //arrange
            var testUsername = "testsu5051";
            var dummyUser = new ApplicationUser() { UserName = testUsername, Email = testUsername + "@seattleu.edu", Id = testUsername};

            //var userStore = new Mock<IUserStore<ApplicationUser>>();
            //var createDummyUser = userStore.Setup(x => x.CreateAsync(dummyUser))
            //    .Returns(Task.FromResult(IdentityResult.Success));
            //var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>()
            //    .Setup(x => x.FindByNameAsync(testUsername))
            //    .ReturnsAsync(dummyUser);
            //var claimsManager = userStore.As<IUserClaimStore<ApplicationUser>>();

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
            Assert.IsNotNull(result, TestContext.TestName);
        }

    }
}
