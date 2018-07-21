using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Portal
{
    [TestClass]
    public class AttendanceUITests
    {
        private string _Controller = "Portal";
        private string _Action = "Attendance";
        private string _DataFirstStudentID = GetFirstStudentID(AssemblyTests.CurrentDriver);

        private string ErrorController = "Portal";
        private string ErrorView = "Roster";

        [TestMethod]
        public void Portal_Settings_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Settings_NavigateToPage_Invalid_No_ID_Should_See_Error_Page()
        {
            AssemblyTests.CurrentDriver.Navigate().GoToUrl(BaseUrl + '/' + _Controller + '/' + _Action);

            ValidatePageTransition(AssemblyTests.CurrentDriver, ErrorController, ErrorView);
        }
    }
}
