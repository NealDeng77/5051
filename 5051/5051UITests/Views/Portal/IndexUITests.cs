﻿using System.Threading.Tasks;
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
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Index_NavigateToPage_Invalid_No_ID_Should_See_Roster_Page()
        {
            NavigateToPageNoValidation(_Controller, _Action);

            ValidatePageTransition(PortalControllerName, RosterViewName);
        }

        [TestMethod]
        public void Portal_Index_Click_All_Nav_Bar_And_Footer_Links()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Nav_Bar_Links(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Footer_Links(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Index_Click_All_On_Page_Links()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //reports link
            ClickActionById("reportsLinkPortalIndex");
            ValidatePageTransition("Admin", "MonthlyReport", _DataFirstStudentID);
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //attendance link
            ClickActionById("attendanceLinkPortalIndex");
            ValidatePageTransition("Portal", "Attendance", _DataFirstStudentID);
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //visit link
            ClickActionById("visitLinkPortalIndex");
            ValidatePageTransition("Portal", "Visit", _DataFirstStudentID);
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //shop link
            ClickActionById("shopLinkPortalIndex");
            ValidatePageTransition("Shop", "Index", _DataFirstStudentID);
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //customize link
            ClickActionById("settingsLinkPortalIndex");
            ValidatePageTransition("Portal", "Settings", _DataFirstStudentID);
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //avatar link
            ClickActionById("avatarLinkPortalIndex");
            ValidatePageTransition("Portal", "Avatar", _DataFirstStudentID);
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

        }
    }
}
