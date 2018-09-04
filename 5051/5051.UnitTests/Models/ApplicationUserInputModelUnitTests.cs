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
    public class ApplicationUserInputUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Models_ApplicationUserInput_Instantiate_Should_Pass()
        {
            // Arange

            // Act
            var result = new ApplicationUserInputModel();

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        //[TestMethod]
        //public void Models_ApplicationUserInput_Instantiate_Get_Set_Should_Pass()
        //{
        //    // Arange

        //    var id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
        //    var myUserInfo = IdentityDataSourceTable.Instance.FindUserByID(id);

        //    var RoleEnum = UserRoleEnum.StudentUser;

        //    // Act
        //    var myReturn = new ApplicationUserInputModel(myUserInfo);
        //    myReturn.Id = myUserInfo.Id;
        //    myReturn.Role = RoleEnum;
        //    myReturn.State = true;

        //    // Reset
        //    DataSourceBackend.Instance.Reset();

        //    // Assert
        //    Assert.IsNotNull(myReturn, TestContext.TestName);
        //    Assert.AreEqual(id, myReturn.Id, TestContext.TestName);
        //    Assert.AreEqual(id, myReturn.Student.Id, TestContext.TestName);

        //    Assert.AreEqual(RoleEnum, myReturn.Role, TestContext.TestName);
        //    Assert.AreEqual(true, myReturn.State, TestContext.TestName);

        //}
        #endregion Instantiate
    }
}
