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
    public class AvatarItemInputUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_AvatarItemInput_Instantiate_Should_Pass()
        {
            // Arange

            // Act
            var result = new AvatarItemInputModel();

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_AvatarItemInput_Instantiate_Get_Set_Should_Pass()
        {
            // Arange
            var expect = new AvatarItemInputModel();
            expect.ItemId = "item";
            expect.Position = AvatarItemCategoryEnum.Accessory;
            expect.StudentId = "student";

            // Act
            var result = new AvatarItemInputModel();
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