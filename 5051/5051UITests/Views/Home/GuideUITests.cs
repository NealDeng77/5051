﻿using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Home
{
    [TestClass]
    public class GuideUITests
    {
        private string _Controller = "Home";
        private string _Action = "Guide";

        [TestMethod]
        public void Home_Guide_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(_Controller, _Action);
        }
    }
}
