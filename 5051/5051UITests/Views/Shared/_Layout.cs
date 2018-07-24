using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Shared
{

    public class _Layout
    {


        public static void Click_All_Nav_Bar_Links(IWebDriver driver, string originalController, string originalAction, string originalData = null)
        {
            //find and click a nav bar item
            //validate the correct page was landed on
            //return to original page

            driver.FindElement(By.Id("AvatarAttendanceLinkNavBar")).Click();
            ValidatePageTransition(driver, "Home", "Index");
            NavigateToPage(driver, originalController, originalAction, originalData);

            driver.FindElement(By.Id("HomeLinkNavBar")).Click();
            ValidatePageTransition(driver, "Home", "Index");
            NavigateToPage(driver, originalController, originalAction, originalData);

            driver.FindElement(By.Id("StudentLinkNavBar")).Click();
            ValidatePageTransition(driver, PortalControllerName, RosterViewName);
            NavigateToPage(driver, originalController, originalAction, originalData);

            driver.FindElement(By.Id("AdminLinkNavBar")).Click();
            ValidatePageTransition(driver, "Admin", "Index");
            NavigateToPage(driver, originalController, originalAction, originalData);

            driver.FindElement(By.Id("KioskModeLinkNavBar")).Click();
            ValidatePageTransition(driver, "Kiosk", "Login");
            NavigateToPage(driver, originalController, originalAction, originalData);
        }

        public static void Click_All_Footer_Links(IWebDriver driver, string originalController, string originalAction, string originalData = null)
        {
            //find and click a footer item
            //validate the correct page was landed on
            //return to original page

            driver.FindElement(By.Id("AboutLinkFooter")).Click();
            ValidatePageTransition(driver, "Home", "About");
            NavigateToPage(driver, originalController, originalAction, originalData);

            driver.FindElement(By.Id("PrivacyPolicyLinkFooter")).Click();
            ValidatePageTransition(driver, "Home", "Privacy");
            NavigateToPage(driver, originalController, originalAction, originalData);

            driver.FindElement(By.Id("ContactLinkFooter")).Click();
            ValidatePageTransition(driver, "Home", "Contact");
            NavigateToPage(driver, originalController, originalAction, originalData);

            driver.FindElement(By.Id("FAQLinkFooter")).Click();
            ValidatePageTransition(driver, "Home", "FAQ");
            NavigateToPage(driver, originalController, originalAction, originalData);

            driver.FindElement(By.Id("UserGuideLinkFooter")).Click();
            ValidatePageTransition(driver, "Home", "Guide");
            NavigateToPage(driver, originalController, originalAction, originalData);
        }


    }
}
