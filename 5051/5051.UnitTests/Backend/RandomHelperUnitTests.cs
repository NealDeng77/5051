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
    }
}
