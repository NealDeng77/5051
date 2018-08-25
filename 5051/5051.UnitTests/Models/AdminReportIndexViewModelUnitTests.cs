using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using Microsoft.AspNet.Identity;
using _5051.Backend;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class AdminReportIndexViewModelUnitTests
    {       
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Models_IndexViewModels_Default_Instantiate_Should_Pass()
        {
            //arrange
            var studentList = StudentBackend.Instance.Index();
            var expectedLeaderBoard = new List<StudentModel>();
            var test = new AdminReportIndexViewModel(studentList);

            //act
            test.Leaderboard = expectedLeaderBoard;

            //assert
            Assert.AreEqual(expectedLeaderBoard, test.Leaderboard, TestContext.TestName);
        }
    }
}
