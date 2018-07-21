using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Portal
{
    [TestClass]
    public class IndexUITests
    {
        private string _Controller = "Portal";
        private string _Action = "Index";
        private string _DataFirstStudentID = GetFirstStudentID(AssemblyTests.CurrentDriver);

        private string ErrorController = "Portal";
        private string ErrorView = "Roster";

        [TestMethod]
        public void Portal_Index_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Index_NavigateToPage_Invalid_No_ID_Should_See_Roster_Page()
        {
            AssemblyTests.CurrentDriver.Navigate().GoToUrl(BaseUrl + '/' + _Controller + '/' + _Action);

            ValidatePageTransition(AssemblyTests.CurrentDriver, ErrorController, ErrorView);
        }

    }
}
