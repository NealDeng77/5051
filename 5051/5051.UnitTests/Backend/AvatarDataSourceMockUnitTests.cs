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
    public class AvatarDataSourceMockUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_AvatarDataSourceMock_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = AvatarDataSourceMock.Instance;

            //var expect = backend;

            // Act
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion Instantiate

        #region delete
        [TestMethod]
        public void Models_AvatarDataSourceMock_Delete_With_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var backend = AvatarDataSourceMock.Instance;
            var expect = false;

            //act
            var result = backend.Delete(null);

            //reset
            backend.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete


    }
}
