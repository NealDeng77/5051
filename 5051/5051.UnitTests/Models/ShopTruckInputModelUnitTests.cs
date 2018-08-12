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
    public class ShopTruckInputUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_ShopTruckInput_Instantiate_Should_Pass()
        {
            // Arange

            // Act
            var result = new ShopTruckInputModel();

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_ShopTruckInput_Instantiate_Get_Set_Should_Pass()
        {
            // Arange
            var expect = new ShopTruckInputModel();
            expect.ItemId = "item";
            expect.Position = FactoryInventoryCategoryEnum.Topper;
            expect.StudentId = "student";

            // Act
            var result = new ShopTruckInputModel();
            result.ItemId = expect.ItemId;
            result.Position = expect.Position;
            result.StudentId = expect.StudentId;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);

            Assert.AreEqual(expect.ItemId, result.ItemId, "Item " + TestContext.TestName);
            Assert.AreEqual(expect.Position, result.Position, "Position " + TestContext.TestName);
            Assert.AreEqual(expect.StudentId, result.StudentId, "Student " + TestContext.TestName);
        }
        #endregion Instantiate
    }
}