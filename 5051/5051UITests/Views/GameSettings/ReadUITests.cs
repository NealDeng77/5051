using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.GameSettings
{
    [TestClass]
    public class ReadUITests
    {
        private string _Controller = "GameSettings";
        private string _Action = "Read";

        [TestMethod]
        public void GameSettings_Read_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action);
        }

        [TestMethod]
        public void GameSettings_Read_NavigateToPage_Invalid_No_ID_Should_See_Error_Page()
        {
            AssemblyTests.CurrentDriver.Navigate().GoToUrl(BaseUrl + '/' + _Controller + '/' + _Action + "/Bogus");

            ValidatePageTransition(AssemblyTests.CurrentDriver, ErrorControllerName, ErrorViewName);
        }

        [TestMethod]
        public void Game_Read_Back_Click_Should_Pass()
        {
            // Get the data for the record to update
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, "Read");

            // Click the Back Button
            ClickActionById(AssemblyTests.CurrentDriver, "BackButton");

            // Vaidate back on Read page
            ValidatePageTransition(AssemblyTests.CurrentDriver, "Admin", "Settings");
        }
    }
}
