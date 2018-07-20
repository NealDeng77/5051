using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests
{
    [TestClass]
    public class RosterUITests
    {
        private string _Controller = "Portal";
        private string _Action = "Roster";

        [TestMethod]
        public void Portal_Visit_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(AssemblyTests.CurrentDriver, _Controller, _Action);
        }

    }
}
