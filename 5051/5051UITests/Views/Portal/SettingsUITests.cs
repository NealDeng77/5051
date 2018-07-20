using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests
{
    [TestClass]
    public class SettingsUITests
    {
        private string _Controller = "Portal";
        private string _Action = "Settings";
        private string _DataFirstStudentID = GetFirstStudentID(AssemblyTests.CurrentDriver);

        private string ErrorController = "Home";
        private string ErrorView = "Error";

        [TestMethod]
        public void Portal_Visit_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Visit_NavigateToPage_Invalid_No_ID_Should_See_Error_Page()
        {
            AssemblyTests.CurrentDriver.Navigate().GoToUrl(BaseUrl + '/' + _Controller + '/' + _Action);

            ValidatePageTransition(AssemblyTests.CurrentDriver, ErrorController, ErrorView);
        }

    }
}
