using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;
using System.Web;
using System.Web.Mvc;

namespace _5051.UnitTests.Models
{

    [TestClass]
    public class UTCConversionsBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        #region FromClientTime
        [TestMethod]
        public void Backend_UTCConversionsBackend_FromClientTime_Valid_Date_HttpContext_Is_Null_Should_Pass()
        {
            //arrange
            var inputLocalDateTime = DateTime.Now;
            var expectDateTime = DateTime.Now.ToUniversalTime();

            //act
            var result = _5051.Backend.UTCConversionsBackend.FromClientTime(inputLocalDateTime);

            //assert
            Assert.AreEqual(expectDateTime.Minute, result.Minute, TestContext.TestName);
            Assert.AreEqual(expectDateTime.Hour, result.Hour, TestContext.TestName);
        }



        #endregion

        #region ToClientTime
        [TestMethod]
        public void Backend_UTCConvensionsBackend_ToClientTime_Valid_Date_Already_Local_Should_Pass()
        {
            //arrange
            var inputDateTime = DateTime.Now;
            var expectDateTime = DateTime.Now;

            //act
            var result = UTCConversionsBackend.ToClientTime(inputDateTime);

            //assert
            Assert.AreEqual(expectDateTime.Minute, result.Minute, TestContext.TestName);
            Assert.AreEqual(expectDateTime.Hour, result.Hour, TestContext.TestName);
        }

        //[TestMethod]
        //public void Backend_UTCConvensionsBackend_ToClientTime_Valid_Date_UTC_Should_Pass()
        //{
        //    //arrange
        //    var inputDateTime = DateTime.UtcNow;
        //    inputDateTime = DateTime.SpecifyKind(inputDateTime, DateTimeKind.Unspecified);
        //    var expectDateTime = DateTime.Now;

        //    //act
        //    var result = UTCConversionsBackend.ToClientTime(inputDateTime);

        //    //assert
        //    Assert.AreEqual(expectDateTime.Minute, result.Minute, TestContext.TestName);
        //    Assert.AreEqual(expectDateTime.Hour, result.Hour, TestContext.TestName);
        //}
        #endregion


    }
}