using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class SendCodeViewModelUnitTests
    {
        public TestContext TestContext { get; set; }


        #region Instantiate
        [TestMethod]
        public void Models_SendCodeViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new SendCodeViewModel();
            var expectSelectedProvider = "Good Provider";
            var expectProviders = new List<SelectListItem> {};
            var expectReturnUrl = "www.theterminator.gov";
            var expectRememberme = true;

            //act
            test.SelectedProvider = expectSelectedProvider;
            test.Providers = expectProviders;
            test.ReturnUrl = expectReturnUrl;
            test.RememberMe = expectRememberme;

            var setResultSelectedProvider = test.SelectedProvider;
            var setResultProviders = test.Providers;
            var setResultReurnUrl = test.ReturnUrl;
            var setResultRememberMe = test.RememberMe;

            //assert
            Assert.AreEqual(expectSelectedProvider, setResultSelectedProvider, TestContext.TestName);
            Assert.AreEqual(expectProviders, setResultProviders, TestContext.TestName);
            Assert.AreEqual(expectReturnUrl, setResultReurnUrl, TestContext.TestName);
            Assert.AreEqual(expectRememberme, setResultRememberMe, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
