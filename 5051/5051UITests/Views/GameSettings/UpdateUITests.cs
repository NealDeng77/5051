using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.GameSettings
{
    [TestClass]
    public class UpdateUITests
    {
        private string _Controller = "GameSettings";
        private string _Action = "Update";

        [TestMethod]
        public void Game_Update_NavigateToPage_Valid_Should_Pass()
        {
            // Get the data for the record to update
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, "Read");

            var element = AssemblyTests.CurrentDriver.FindElement(By.Id("Id"));
            var elementval = element.GetAttribute("value");

            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, elementval);
        }

        [TestMethod]
        public void Game_Update_Back_Click_Should_Pass()
        {
            // Get the data for the record to update
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, "Read");

            var element = AssemblyTests.CurrentDriver.FindElement(By.Id("Id"));
            var elementval = element.GetAttribute("value");

            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, elementval);

            // Click the Back Button
            ClickActionById(AssemblyTests.CurrentDriver, "BackButton");

            // Vaidate back on Read page
            ValidatePageTransition(AssemblyTests.CurrentDriver, _Controller, "Read");
        }

        [TestMethod]
        public void Game_Update_NavigateToPage_Invalid_No_ID_Should_See_Error_Page()
        {
            AssemblyTests.CurrentDriver.Navigate().GoToUrl(BaseUrl + '/' + _Controller + '/' + _Action);

            ValidatePageTransition(AssemblyTests.CurrentDriver, ErrorControllerName, ErrorViewName);
        }
    }
}
