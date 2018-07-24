﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
 
using _5051;
using _5051.Controllers;
using _5051.Backend;
using _5051.Models;
using System.Web.Helpers;
using System.Reflection;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class GameControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_Game_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new GameController();

            // Act
            var result = controller.GetType();

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(result, new GameController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region PostSimulationRegion
        [TestMethod]
        public void Controller_Game_Post_Simulation_Invalid_Model_Should_Send_Back_For_Edit()
        {
            // Arrange
            var controller = new GameController();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var data = DataSourceBackend.Instance.GameBackend.GetDefault();

            // Act
            var result = (JsonResult)controller.Simulation(data);
            var dataResult = result.Data.GetType().GetProperty("Error", BindingFlags.Instance | BindingFlags.Public);
            var dataVal = dataResult.GetValue(result.Data, null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(true, dataVal, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Game_Post_Simulation_Null_Data_Should_Return_Error()
        {
            // Arrange
            var controller = new GameController();

            // Act
            var result = (JsonResult)controller.Simulation((GameModel)null);
            var data = result.Data.GetType().GetProperty("Error", BindingFlags.Instance | BindingFlags.Public);
            var dataVal = data.GetValue(result.Data, null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(true, dataVal, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Game_Post_Simulation_Null_Id_Should_Return_Error()
        {
            // Arrange
            var controller = new GameController();

            // Get default GameModel
            var data = DataSourceBackend.Instance.GameBackend.GetDefault();
            data.Id = string.Empty;

            // Act
            var result = (JsonResult)controller.Simulation(data);
            var dataResult = result.Data.GetType().GetProperty("Error", BindingFlags.Instance | BindingFlags.Public);
            var dataVal = dataResult.GetValue(result.Data, null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(true, dataVal, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Game_Post_Simulation_Default_Should_Return_Json_Zero_Iterations()
        {
            // Arrange
            var controller = new GameController();

            // Get default GameModel
            var data = DataSourceBackend.Instance.GameBackend.GetDefault();

            // Act
            var result = (JsonResult)controller.Simulation(data);
            var dataResult = result.Data.GetType().GetProperty("data", BindingFlags.Instance | BindingFlags.Public);
            var dataVal = dataResult.GetValue(result.Data, null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("0", dataVal, TestContext.TestName);
        }
        #endregion PostSimulationRegion
    }
}

// Store for later reference
//var expect = new JsonResult
//{
//    Data = new
//    {
//        Error = false,
//        Msg = "OK",
//        data = "0",
//    }
//};
