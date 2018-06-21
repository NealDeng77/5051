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
    public class KioskSettingsUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_KioskSettings_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var myModel = new KioskSettingsModel();

            // Act
            var result = myModel;

            // Assert
            Assert.AreEqual(result.Password, new KioskSettingsModel().Password, TestContext.TestName);
        }

        #endregion Instantiate
    }
}
