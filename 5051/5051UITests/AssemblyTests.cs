using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace _5051UITests
{
    [TestClass]
    public class AssemblyTests
    {
        public static IWebDriver CurrentDriver = new ChromeDriver(Extensions.ChromeDriverLocation);
        public static ChromeOptions Options = new ChromeOptions();

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            //set implicit wait for driver
            CurrentDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Extensions.WaitTime);
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            CurrentDriver.Quit();
        }
    }
}
