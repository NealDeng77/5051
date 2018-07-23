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
        public const string LocalUrl = "http://localhost:59052";
        public const string BaseUrl = LocalUrl;

        public static string ChromeDriverLocation = "../../ChromeDriver";

        public const int WaitTime = 10;

        public const string PortalControllerName = "Portal";
        public const string RosterViewName = "Roster";
        public const string ErrorControllerName = "Home";
        public const string ErrorViewName = "Error";

        /// <summary>
        /// Validates that the controller / action / data page the driver is on 
        /// is the expected page
        /// </summary>
        public static bool ClickActionById(IWebDriver driver, string elementId)
        {
            var element = driver.FindElement(By.Id(elementId));
            element.Click();
            return true;
        }

        /// <summary>
        /// Validates that the controller / action / data page the driver is on 
        /// is the expected page
        /// </summary>
        public static bool ValidatePageTransition(IWebDriver driver, string controller, string action, string data = null)
        {
            driver.FindElement(By.Id("Page-Done"));
            driver.FindElement(By.Id("Area--Done"));
            driver.FindElement(By.Id("Controller-" + controller + "-Done"));
            driver.FindElement(By.Id("View-" + action + "-Done"));

            return true;
        }

        /// <summary>
        /// Navigates to the driver to the given controller / action / data page 
        /// and validates that the page was landed on
        /// </summary>
        public static bool NavigateToPage(IWebDriver driver, string controller, string action, string data = null)
        {

            //navigate to the desired page
            driver.Navigate().GoToUrl(Extensions.BaseUrl + "/" + controller + "/" + action + "/" + data);

            //check that page is the right page
            ValidatePageTransition(driver, controller, action);

            return true;
        }

        /// <summary>
        /// Returns the ID of the first displayed Student
        /// </summary>
        public static string GetFirstStudentID(IWebDriver driver)
        {
            //the original page
            var beforeURL = driver.Url;

            NavigateToPage(driver, "Student", "Index");
            var studentBoxes = driver.FindElements(By.Id("studentContainer"));
            var firstElementBox = studentBoxes.FirstOrDefault();
            var resultA = firstElementBox.FindElements(By.TagName("a"));

            //if this fails, means there are no students
            resultA.FirstOrDefault().Click();

            var pageURL = driver.Url;
            var testurl1 = pageURL.TrimStart(BaseUrl.ToCharArray());
            var testurl2 = testurl1.TrimStart('/');
            var testurl3 = testurl2.TrimStart("Student".ToCharArray());
            var testurl4 = testurl3.TrimStart('/');
            var testurl5 = testurl4.TrimStart("Read".ToCharArray());
            var testurl6 = testurl5.TrimStart('/');
            var firstStudentID = testurl6;

            //these two lines confirm that the ID is correct
            NavigateToPage(driver, "Student", "Index");
            NavigateToPage(driver, "student", "read", firstStudentID);

            //return to the original page
            driver.Navigate().GoToUrl(beforeURL);

            return firstStudentID;
        }
    }
}
