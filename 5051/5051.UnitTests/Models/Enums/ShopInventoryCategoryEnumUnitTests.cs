using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class FactoryInventoryCategoryEnumUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_FactoryInventoryCategoryEnumUnitTests_Values_Should_Pass()
        {
            // Assert

            // Make sure there are no additional values
            var enumCount = FactoryInventoryCategoryEnum.GetNames(typeof(FactoryInventoryCategoryEnum)).Length;
            Assert.AreEqual(6, enumCount, TestContext.TestName);

            // Check each value against their expected value.
            Assert.AreEqual(0, (int)FactoryInventoryCategoryEnum.Unknown, TestContext.TestName);
            Assert.AreEqual(1, (int)FactoryInventoryCategoryEnum.Music, TestContext.TestName);
            Assert.AreEqual(2, (int)FactoryInventoryCategoryEnum.Entertainment, TestContext.TestName);
            Assert.AreEqual(5, (int)FactoryInventoryCategoryEnum.Food, TestContext.TestName);
            Assert.AreEqual(6, (int)FactoryInventoryCategoryEnum.Furniture, TestContext.TestName);
            Assert.AreEqual(7, (int)FactoryInventoryCategoryEnum.Decoration, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
