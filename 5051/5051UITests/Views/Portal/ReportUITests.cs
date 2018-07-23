using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Portal
{
    [TestClass]
    public class ReportUITests
    {
        private string _Controller = "Portal";
        private string _Action = "Report";
        private string _DataFirstStudentID = GetFirstStudentID(AssemblyTests.CurrentDriver);

        [TestMethod]
        public void Portal_Report_NavigateToPage_Valid_Should_Pass()
        {
            //NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);

            //portal/report/id redirects to admin/monthlyreport/id, so must manually validate page transition
            AssemblyTests.CurrentDriver.Navigate().GoToUrl(BaseUrl + "/" + _Controller + "/" + _Action + "/" + _DataFirstStudentID);
            ValidatePageTransition(AssemblyTests.CurrentDriver, "Admin", "MonthlyReport", _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Report_NavigateToPage_Invalid_No_ID_Should_See_Error_Page()
        {
            AssemblyTests.CurrentDriver.Navigate().GoToUrl(BaseUrl + '/' + _Controller + '/' + _Action);

            ValidatePageTransition(AssemblyTests.CurrentDriver, ErrorControllerName, ErrorViewName);
        }

    }
}
