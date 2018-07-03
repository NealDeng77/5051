using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class VerifyCodeViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_VerifyCodeViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new VerifyCodeViewModel();
            var expectProvider = "Good Provider";
            var expectCode = "Good Code";
            var expectReturnUrl = "www.deliciousurl.gov";
            var expectRememberBrower = true;
            var expectRememberMe = true;

            //act
            test.Provider = expectProvider;
            test.Code = expectCode;
            test.ReturnUrl = expectReturnUrl;
            test.RememberBrowser = expectRememberBrower;
            test.RememberMe = expectRememberMe;

            var setResultProvider = test.Provider;
            var setResultCode = test.Code;
            var setResultReturnUrl = test.ReturnUrl;
            var setResultRememberBrowser = test.RememberBrowser;
            var setResultRememberMe = test.RememberMe;

            //assert
            Assert.AreEqual(expectProvider, setResultProvider, TestContext.TestName);
            Assert.AreEqual(expectCode, setResultCode, TestContext.TestName);
            Assert.AreEqual(expectReturnUrl, setResultReturnUrl, TestContext.TestName);
            Assert.AreEqual(expectRememberBrower, setResultRememberBrowser, TestContext.TestName);
            Assert.AreEqual(expectRememberMe, setResultRememberMe, TestContext.TestName);
        }
        #endregion Instantiate

    }
}
