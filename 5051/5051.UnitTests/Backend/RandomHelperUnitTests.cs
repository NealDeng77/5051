using _5051.Backend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class RandomHelperUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Backend_RandomHelper_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = RandomHelper.Instance;

            //var expect = backend;

            // Act
            var result = backend;

            //Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate

        #region SetForcedNumber
        [TestMethod]
        public void Backend_RandomHelper_SetForcedNumber_Should_Pass()
        {
            // Arrange
            var backend = RandomHelper.Instance;

            // Act
            var expect = true;
            RandomHelper.SetForcedNumber();

            //Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, RandomHelper.isSetForcedNumber, TestContext.TestName);
        }
        #endregion SetForcedNumber

        #region GetRandomNumber
        [TestMethod]
        public void Backend_RandomHelper_GetRandomNumber_Data_Is_Valid_Should_Pass()
        {
            // Arrange
            var backend = RandomHelper.Instance;

            // Act
            RandomHelper.SetForcedNumber();
            var expect = RandomHelper.GetRandomNumber();

            //Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(expect, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_RandomHelper_GetRandomNumber_Data_Is_Not_Valid_Should_Fail()
        {
            // Arrange
            var backend = RandomHelper.Instance;

            // Act
            var result = RandomHelper.GetRandomNumber();
            var expect = -1;

            //Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion GetRandomNumber
    }
}
