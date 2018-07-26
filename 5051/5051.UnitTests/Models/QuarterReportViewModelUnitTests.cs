using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class QuarterReportViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Models_QuarterReportViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new QuarterReportViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_QuarterReportViewModel_InitializeQuarters_Should_Pass()
        {
            // Act
            var data = new QuarterReportViewModel();
            data.Quarters = new List<SelectListItem>();

            // Assert
            Assert.IsNotNull(data.Quarters, TestContext.TestName);
        }

        [TestMethod]
        public void Models_QuarterReportViewModel_SelectedQuarterId_is_1_Should_Pass()
        {
            // Act
            var data = new QuarterReportViewModel();
            data.SelectedQuarterId = 1;
            var expect = 1;

            // Assert
            Assert.AreEqual(expect, data.SelectedQuarterId, TestContext.TestName);
        }
    }
}
