using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace _5051UITests
{
    public class Extensions
    {
        public const string LocalUrl = "http://localhost:59052/";
        public const string BaseUrl = LocalUrl;

        public static string ChromeDriverLocation = "../../ChromeDriver";

        public const int WaitTime = 10;

        public static bool ValidatePageTransition(IWebDriver driver, string controller, string action, string data = null)
        {
            driver.FindElement(By.Id("Page-Done"));
            driver.FindElement(By.Id("Area--Done"));
            driver.FindElement(By.Id("Controller-" + controller + "-Done"));
            driver.FindElement(By.Id("View-" + action + "-Done"));

            return true;
        }

        public static bool NavigateToPage(IWebDriver driver, string controller, string action, string data = null)
        {

            //navigate to the desired page
            driver.Navigate().GoToUrl(Extensions.BaseUrl + "/" + controller + "/" + action + "/" + data);

            //check that page is the right page
            ValidatePageTransition(driver, controller, action);

            return true;
        }
    }
}
