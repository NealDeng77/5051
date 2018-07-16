using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using System.Web.Mvc;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class ChangePasswordViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_ChangePasswordViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new ChangePasswordViewModel();

            var ExpectConfirmPassword = "123";
            var ExpectNewPassword = "abc";
            var ExpectOldPassword = "xyz";


            //act
            test.ConfirmPassword = "123";
            test.NewPassword = "abc";
            test.OldPassword = "xyz";

            //assert
            Assert.AreEqual(ExpectConfirmPassword, test.ConfirmPassword, "Confirm " + TestContext.TestName);
            Assert.AreEqual(ExpectNewPassword, test.NewPassword, "New " + TestContext.TestName);
            Assert.AreEqual(ExpectOldPassword, test.OldPassword, "Old " + TestContext.TestName);
        }
        #endregion Instantiate

    }
}