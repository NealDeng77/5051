using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class AvatarBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        #region delete
        [TestMethod]
        public void Models_AvatarBackend_Delete_With_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = Backend.AvatarBackend.Instance;
            var expect = false;

            //act
            var result = test.Delete(null);

            //reset
            test.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete


    }
}
