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
    public class AvatarBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        #region delete
        [TestMethod]
        public void Models_AvatarBackend_Delete_With_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = Backend.AvatarBackend.Instance;
            var expect = false;

            //act
            var result = test.Delete(null);

            //reset
            test.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region GetAvatarListItem
        [TestMethod]
        public void Backend_AvatarBackend_GetAvatarListItem_ID_Null_Should_Pass()
        {
            //arrange
            var test = Backend.AvatarBackend.Instance;

            //act
            var result = test.GetAvatarListItem(null);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion GetAvatarListItem

        #region GetAvatarUri
        [TestMethod]
        public void Backend_AvatarBackend_GetAvatarUri_Valid_Data_Should_Pass()
        {
            //arrange
            var test = Backend.AvatarBackend.Instance;
            var testID = test.GetFirstAvatarId();

            //act
            var result = test.GetAvatarUri(testID);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarBackend_GetAvatarUri_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = Backend.AvatarBackend.Instance;

            //act
            var result = test.GetAvatarUri(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarBackend_GetAvatarUri_Invalid_ID_Should_Fail()
        {
            //arrange
            var data = new AvatarModel();
            var test = Backend.AvatarBackend.Instance;
            data.Id = "bogus";

            //act
            var result = test.GetAvatarUri(data.Id);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion GetAvatarUri

        #region update
        [TestMethod]
        public void Models_AvatarBackend_Update_With_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = Backend.AvatarBackend.Instance;

            //act
            var result = test.Update(null);

            //reset
            test.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion update

        #region index
        [TestMethod]
        public void Backend_AvatarBackend_Index_Valid_Should_Pass()
        {
            //arrange
            var test = Backend.AvatarBackend.Instance;

            //act
            var result = test.Index();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion index

        #region read
        [TestMethod]
        public void Backend_AvatarBackend_Read_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = Backend.AvatarBackend.Instance;

            //act
            var result = test.Read(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarBackend_Read_Valid_ID_Should_Pass()
        {
            //arrange
            var test = Backend.AvatarBackend.Instance;
            var testID = test.GetFirstAvatarId();

            //act
            var result = test.Read(testID);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read
    }
}
