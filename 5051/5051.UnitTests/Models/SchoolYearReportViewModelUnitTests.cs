using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class SchoolYearReportViewModelUnitTests
    {

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Models_StudentReportViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new SchoolYearReportViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
    }
}
