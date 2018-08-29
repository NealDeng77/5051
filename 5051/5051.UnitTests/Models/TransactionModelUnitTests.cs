using _5051.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class TransactionModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_TransactionModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new TransactionModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate



        #region Update


        #endregion Update
    }
}
