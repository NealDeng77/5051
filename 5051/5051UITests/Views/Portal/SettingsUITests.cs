using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Portal
{
    [TestClass]
    public class SettingsUITests
    {
        private string _Controller = "Portal";
        private string _Action = "Settings";
        private string _DataFirstStudentID = GetFirstStudentID(AssemblyTests.CurrentDriver);

        [TestMethod]
        public void Portal_Settings_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Settings_NavigateToPage_Invalid_No_ID_Should_See_Error_Page()
        {
            NavigateToPageNoValidation(_Controller, _Action);

            ValidatePageTransition(ErrorControllerName, ErrorViewName);
        }

        [TestMethod]
        public void Portal_Settings_Click_All_Nav_Bar_And_Footer_Links()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Nav_Bar_Links(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Footer_Links(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Settings_Click_All_On_Page_Links()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //the update button
            AssemblyTests.CurrentDriver.FindElement(By.Id("updateSubmitButton")).Click();
            ValidatePageTransition(_Controller, "Index", _DataFirstStudentID);

        }
    }
}
