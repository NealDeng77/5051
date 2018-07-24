﻿using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Portal
{
    [TestClass]
    public class LoginUITests
    {
        private string _Controller = "Portal";
        private string _Action = "Login";
        private string _DataFirstStudentID = GetFirstStudentID(AssemblyTests.CurrentDriver);

        [TestMethod]
        public void Portal_Login_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Login_NavigateToPage_Invalid_No_ID_Should_See_Roster_Page()
        {
            NavigateToPageNoValidation(_Controller, _Action);

            ValidatePageTransition(PortalControllerName, RosterViewName);
        }

        [TestMethod]
        public void Portal_Login_Click_All_Nav_Bar_And_Footer_Links()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Nav_Bar_Links(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Footer_Links(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Login_Click_All_On_Page_Links()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //cancel link
            ClickActionById("cancelLoginButton");
            ValidatePageTransition("Portal", "Roster");

            //login link
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);
            //anylogin info would be put here if there was any AKA username/password
            ClickActionById("loginButton");
            ValidatePageTransition("Portal", "Index", _DataFirstStudentID);

        }
    }
}
