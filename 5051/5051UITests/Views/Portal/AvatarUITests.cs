using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Portal
{
    [TestClass]
    public class AvatarUITests
    {
        private string _Controller = "Portal";
        private string _Action = "Avatar";
        private string _DataFirstStudentID = GetFirstStudentID(AssemblyTests.CurrentDriver);

        [TestMethod]
        public void Portal_Avatar_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Avatar_NavigateToPage_Invalid_No_ID_Should_See_Roster_Page()
        {
            NavigateToPageNoValidation(_Controller, _Action);

            ValidatePageTransition(ErrorControllerName, ErrorViewName);
        }

        [TestMethod]
        public void Portal_Avatar_Click_All_Nav_Bar_And_Footer_Links()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Nav_Bar_Links(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Footer_Links(AssemblyTests.CurrentDriver, _Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Avatar_Click_All_On_Page_Links()
        {
            var numAvatarIdsOnIndexPage = 1;

            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //each avatar available should be clickable, user is only level 1, so only 3 avatars available
            var avatarObjects = AssemblyTests.CurrentDriver.FindElements(By.Id("AvatarId"));

            var avatarID0 = avatarObjects[0].GetAttribute("value");
            var avatarID1 = avatarObjects[1].GetAttribute("value");
            var avatarID2 = avatarObjects[2].GetAttribute("value");

            ClickActionById(avatarID2);
            ValidatePageTransition("Portal", "Index", _DataFirstStudentID);
            //confirm that the id of the users avatar image changed
            var result = ValidateIdExists(avatarID2);
            Assert.AreEqual(numAvatarIdsOnIndexPage, result);

            NavigateToPage(_Controller, _Action, _DataFirstStudentID);
            ClickActionById(avatarID1);
            ValidatePageTransition("Portal", "Index", _DataFirstStudentID);
            //confirm that the id of the users avatar image changed
            result = ValidateIdExists(avatarID1);
            Assert.AreEqual(numAvatarIdsOnIndexPage, result);

            NavigateToPage(_Controller, _Action, _DataFirstStudentID);
            ClickActionById(avatarID0);
            ValidatePageTransition("Portal", "Index", _DataFirstStudentID);
            //confirm that the id of the users avatar image changed
            result = ValidateIdExists(avatarID0);
            Assert.AreEqual(numAvatarIdsOnIndexPage, result);
        }
    }
}
