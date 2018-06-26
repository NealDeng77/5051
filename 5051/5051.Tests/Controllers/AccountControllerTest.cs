using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Controllers;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_Account_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new AccountController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate
    }
}
