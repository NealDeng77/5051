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
    public class RegisterViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_RegisterViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new RegisterViewModel();
            var expectEmail = "test@gmail.com";
            var expectPassword = "passWord23!";
            var expectConfirmPassword = expectPassword;

            //act
            var resultEmail = test.Email;
            var resultPassword = test.Password;
            var resultConfirmPassword = test.ConfirmPassword;

            test.Email = expectEmail;
            test.Password = expectPassword;
            test.ConfirmPassword = expectConfirmPassword;

            var setResultEmail = test.Email;
            var setResultPassword = test.Password;
            var setResultConfirmPassword = test.ConfirmPassword;

            //assert
            Assert.IsNull(resultEmail, TestContext.TestName);
            Assert.IsNull(resultPassword, TestContext.TestName);
            Assert.IsNull(resultConfirmPassword, TestContext.TestName);

            Assert.AreEqual(expectEmail, setResultEmail, TestContext.TestName);
            Assert.AreEqual(expectPassword, setResultPassword, TestContext.TestName);
            Assert.AreEqual(expectConfirmPassword, setResultConfirmPassword, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
