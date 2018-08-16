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

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class IdentityBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        //[TestMethod]
        //public void Backend_IdentityBackend_CreateSupportUser_Should_Pass()
        //{
        //    //arrange
        //    var backend = new IdentityBackend();
        //    var userStore = new IUserStore<ApplicationUser>();

        //    //act
        //    var result = backend.CreateNewSupportUser("su5051");

        //    //assert
        //    Assert.IsNotNull(result, TestContext.TestName);
        //}

    }
}
