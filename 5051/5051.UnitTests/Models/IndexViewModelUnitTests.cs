using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using Microsoft.AspNet.Identity;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class IndexViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Models_IndexViewModels_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new IndexViewModel();

            var expectHasPassword = true;
            var expectLogins = new List<UserLoginInfo>();
            var expectPhoneNumber = "749 8100";
            var expectTwoFactor = true;
            var expectBrowserRemembered = true;

            //act
            test.HasPassword = expectHasPassword;
            test.Logins = expectLogins;
            test.PhoneNumber = expectPhoneNumber;
            test.TwoFactor = expectTwoFactor;
            test.BrowserRemembered = expectBrowserRemembered;

            var setResultHasPassword = test.HasPassword;
            var setResultLogins = test.Logins;
            var setResultPhoneNumber = test.PhoneNumber;
            var setResultTwoFactor = test.TwoFactor;
            var setResultBrowserRemembered = test.BrowserRemembered;

            //assert
            Assert.AreEqual(expectHasPassword, setResultHasPassword, TestContext.TestName);
            Assert.AreEqual(expectLogins, setResultLogins, TestContext.TestName);
            Assert.AreEqual(expectPhoneNumber, setResultPhoneNumber, TestContext.TestName);
            Assert.AreEqual(expectTwoFactor, setResultTwoFactor, TestContext.TestName);
            Assert.AreEqual(expectBrowserRemembered, setResultBrowserRemembered, TestContext.TestName);
        }
    }
}
