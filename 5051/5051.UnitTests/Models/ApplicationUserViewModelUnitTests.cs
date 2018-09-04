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
    public class ApplicationUserViewUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Models_ApplicationUserView_Instantiate_Should_Pass()
        {
            // Arange
            var id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            var userInfo = IdentityDataSourceTable.Instance.FindUserByID(id);

            // Act
            var result = new ApplicationUserViewModel(userInfo);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
