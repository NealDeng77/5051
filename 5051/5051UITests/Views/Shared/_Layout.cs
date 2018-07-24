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


        public static void Click_All_Nav_Bar_Links(string originalController, string originalAction, string originalData = null)
        {
            //find and click a nav bar item
            //validate the correct page was landed on
            //return to original page

            AssemblyTests.CurrentDriver.FindElement(By.Id("AvatarAttendanceLinkNavBar")).Click();
            ValidatePageTransition("Home", "Index");
            NavigateToPage(originalController, originalAction, originalData);

            AssemblyTests.CurrentDriver.FindElement(By.Id("HomeLinkNavBar")).Click();
            ValidatePageTransition("Home", "Index");
            NavigateToPage(originalController, originalAction, originalData);

            AssemblyTests.CurrentDriver.FindElement(By.Id("StudentLinkNavBar")).Click();
            ValidatePageTransition(PortalControllerName, RosterViewName);
            NavigateToPage(originalController, originalAction, originalData);

            AssemblyTests.CurrentDriver.FindElement(By.Id("AdminLinkNavBar")).Click();
            ValidatePageTransition("Admin", "Index");
            NavigateToPage(originalController, originalAction, originalData);

            AssemblyTests.CurrentDriver.FindElement(By.Id("KioskModeLinkNavBar")).Click();
            ValidatePageTransition("Kiosk", "Login");
            NavigateToPage(originalController, originalAction, originalData);
        }

        public static void Click_All_Footer_Links(string originalController, string originalAction, string originalData = null)
        {
            //find and click a footer item
            //validate the correct page was landed on
            //return to original page

            AssemblyTests.CurrentDriver.FindElement(By.Id("AboutLinkFooter")).Click();
            ValidatePageTransition("Home", "About");
            NavigateToPage(originalController, originalAction, originalData);

            AssemblyTests.CurrentDriver.FindElement(By.Id("PrivacyPolicyLinkFooter")).Click();
            ValidatePageTransition("Home", "Privacy");
            NavigateToPage(originalController, originalAction, originalData);

            AssemblyTests.CurrentDriver.FindElement(By.Id("ContactLinkFooter")).Click();
            ValidatePageTransition("Home", "Contact");
            NavigateToPage(originalController, originalAction, originalData);

            AssemblyTests.CurrentDriver.FindElement(By.Id("FAQLinkFooter")).Click();
            ValidatePageTransition("Home", "FAQ");
            NavigateToPage(originalController, originalAction, originalData);

            AssemblyTests.CurrentDriver.FindElement(By.Id("UserGuideLinkFooter")).Click();
            ValidatePageTransition("Home", "Guide");
            NavigateToPage(originalController, originalAction, originalData);
        }


    }
}
