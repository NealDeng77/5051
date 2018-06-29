using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Models.Enums;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class EmotionStatusEnumUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_EmotionStatusEnumUnitTests_Values_Should_Pass()
        {

            // Assert
            Assert.AreEqual(5, (int)EmotionStatusEnum.VeryHappy, TestContext.TestName);
            Assert.AreEqual(4, (int)EmotionStatusEnum.Happy, TestContext.TestName);
            Assert.AreEqual(3, (int)EmotionStatusEnum.Neutral, TestContext.TestName);
            Assert.AreEqual(2, (int)EmotionStatusEnum.Sad, TestContext.TestName);
            Assert.AreEqual(1, (int)EmotionStatusEnum.VerySad, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
