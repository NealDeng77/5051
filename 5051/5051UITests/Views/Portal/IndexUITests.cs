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

        [TestMethod]
        public void Portal_Index_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Index_NavigateToPage_Invalid_No_ID_Should_See_Roster_Page()
        {
            AssemblyTests.CurrentDriver.Navigate().GoToUrl(BaseUrl + '/' + _Controller + '/' + _Action);

            ValidatePageTransition(AssemblyTests.CurrentDriver, PortalControllerName, RosterViewName);
        }

        [TestMethod]
        public void Portal_Index_Click_All_Nav_Bar_And_Footer_Links()
        {
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Nav_Bar_Links(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Footer_Links(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Index_Click_All_On_Page_Links()
        {
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);

            //reports link
            ClickActionById("reportsLinkPortalIndex");
            ValidatePageTransition(AssemblyTests.CurrentDriver, "Admin", "MonthlyReport", _DataFirstStudentID);
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);

            //attendance link
            ClickActionById("attendanceLinkPortalIndex");
            ValidatePageTransition(AssemblyTests.CurrentDriver, "Portal", "Attendance", _DataFirstStudentID);
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);

            //visit link
            ClickActionById("visitLinkPortalIndex");
            ValidatePageTransition(AssemblyTests.CurrentDriver, "Portal", "Visit", _DataFirstStudentID);
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);

            //shop link
            ClickActionById("shopLinkPortalIndex");
            ValidatePageTransition(AssemblyTests.CurrentDriver, "Shop", "Index", _DataFirstStudentID);
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);

            //customize link
            ClickActionById("settingsLinkPortalIndex");
            ValidatePageTransition(AssemblyTests.CurrentDriver, "Portal", "Settings", _DataFirstStudentID);
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);

            //avatar link
            ClickActionById("avatarLinkPortalIndex");
            ValidatePageTransition(AssemblyTests.CurrentDriver, "Portal", "Avatar", _DataFirstStudentID);
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);

        }
    }
}
