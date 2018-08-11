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
    public class AvatarCompositModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_AvatarComposit_Default_Instantiate_Get_Set_Should_Pass()
        {
            // Arange
            string AvatarAccessoryUri = "/content/avatar/Accessory0.png";
            string AvatarCheeksUri = "/content/avatar/Cheeks0.png";
            string AvatarExpressionUri = "/content/avatar/Expression0.png";
            string AvatarHairBackUri = "/content/avatar/Hair2_short_black.png";
            string AvatarHairFrontUri = "/content/avatar/Hair1_straight_black.png";
            string AvatarHeadUri = "/content/avatar/Head0.png";
            string AvatarShirtUri = "/content/avatar/Shirt_short_white.png";

            // Act
            var result = new AvatarCompositModel();

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);

            Assert.AreEqual(AvatarAccessoryUri, result.AvatarAccessoryUri, "AvatarAccessoryUri " + TestContext.TestName);
            Assert.AreEqual(AvatarCheeksUri, result.AvatarCheeksUri, "AvatarCheeksUri " + TestContext.TestName);
            Assert.AreEqual(AvatarExpressionUri,result.AvatarExpressionUri, "AvatarExpressionUri " + TestContext.TestName);
            Assert.AreEqual(AvatarHairBackUri, result.AvatarHairBackUri, "AvatarHairBackUri " + TestContext.TestName);
            Assert.AreEqual(AvatarHairFrontUri, result.AvatarHairFrontUri, "AvatarHairFrontUri " + TestContext.TestName);
            Assert.AreEqual(AvatarHeadUri, result.AvatarHeadUri, "AvatarHeadUri " + TestContext.TestName);
            Assert.AreEqual(AvatarShirtUri, result.AvatarShirtUri, "AvatarShirtUri " + TestContext.TestName);
        }

        #endregion Instantiate
    }
}
