using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Home
{
    [TestClass]
    public class IndexUITests
    {
        private string _Controller = "Home";
        private string _Action = "Index";

        [TestMethod]
        public void Home_Index_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(_Controller, _Action);
        }

    }
}
