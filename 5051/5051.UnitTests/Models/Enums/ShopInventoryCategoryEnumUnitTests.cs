using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class ShopInventoryCategoryEnumUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_ShopInventoryCategoryEnumUnitTests_Values_Should_Pass()
        {
            // Assert

            // Make sure there are no additional values
            var enumCount = ShopInventoryCategoryEnum.GetNames(typeof(ShopInventoryCategoryEnum)).Length;
            Assert.AreEqual(3, enumCount, TestContext.TestName);

            // Check each value against their expected value.
            Assert.AreEqual(0, (int)ShopInventoryCategoryEnum.Unknown, TestContext.TestName);
            Assert.AreEqual(1, (int)ShopInventoryCategoryEnum.Music, TestContext.TestName);
            Assert.AreEqual(2, (int)ShopInventoryCategoryEnum.Entertainment, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
