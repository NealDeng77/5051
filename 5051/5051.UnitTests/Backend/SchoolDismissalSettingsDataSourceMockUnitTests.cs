﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class SchoolDismissalSettingsDataSourceMockUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_SchoolDismissalSettingsDataSourceMock_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = SchoolDismissalSettingsDataSourceMock.Instance;

            //var expect = backend;

            // Act
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion Instantiate
    }
}