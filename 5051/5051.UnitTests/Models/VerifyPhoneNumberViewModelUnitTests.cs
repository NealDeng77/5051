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
    public class VerifyPhoneNumberViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region code
        [TestMethod]
        public void Models_VerifyPhoneNumberViewModel_Code_Get_Set_Should_Pass()
        {
            //arrange
            var test = new VerifyPhoneNumberViewModel();
            var expect = "1 916";

            //act
            var result = test.Code;
            test.Code = expect;
            var setResult = test.Code;

            //assert
            Assert.IsNull(result, TestContext.TestName);
            Assert.AreEqual(expect, setResult, TestContext.TestName);
        }
        #endregion code

        #region PhoneNumber
        [TestMethod]
        public void Models_VerifyPhoneNumberViewModel_PhoneNumber_Get_Set_Should_Pass()
        {
            //arrange
            var test = new VerifyPhoneNumberViewModel();
            var expect = "742 8100";

            //act
            var result = test.PhoneNumber;
            test.PhoneNumber = expect;
            var setResult = test.PhoneNumber;

            //assert
            Assert.IsNull(result, TestContext.TestName);
            Assert.AreEqual(expect, setResult, TestContext.TestName);
        }
        #endregion PhoneNumber

    }
}
