using System;
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
    public class ShopTruckViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_ShopTruckViewModel_Default_Instantiate_Get_Set_Should_Pass()
        {
            //arrange
            // Get inventory Item
            // Add it to a Truck Model
            // Convert the Truck Model to a Truck View Model
            // Check the URI to see if it converted over correctly

            var InventoryList = DataSourceBackend.Instance.FactoryInventoryBackend.Index();

            var ShopTruck = new ShopTruckModel();
            var ItemId = InventoryList.FirstOrDefault().Id;

            ShopTruck.Truck = ItemId;
            ShopTruck.Wheels = ItemId;
            ShopTruck.Topper= ItemId;
            ShopTruck.Trailer= ItemId;
            ShopTruck.Menu= ItemId;
            ShopTruck.Sign= ItemId;
            ShopTruck.StudentId = "testdata";

            var expect = InventoryList.FirstOrDefault().Uri;

            // Act
            var result = new ShopTruckViewModel(ShopTruck);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result.Wheels, "Wheels " + TestContext.TestName);
            Assert.AreEqual(expect, result.Truck, "Truck " + TestContext.TestName);
            Assert.AreEqual(expect, result.Topper, "Topper " + TestContext.TestName);
            Assert.AreEqual(expect, result.Trailer, "Trailer " + TestContext.TestName);
            Assert.AreEqual(expect, result.Menu, "Menu " + TestContext.TestName);
            Assert.AreEqual(expect, result.Sign, "Sign " + TestContext.TestName);
            Assert.AreEqual("testdata", result.StudentId, "StudentId " + TestContext.TestName);
        }

        [TestMethod]
        public void Models_ShopTruckViewModel_Default_Null_Instantiate_Get_Set_Should_Pass()
        {
            //arrange
            // Get inventory Item
            // Add it to a Truck Model
            // Convert the Truck Model to a Truck View Model
            // Check the URI to see if it converted over correctly

            var InventoryList = DataSourceBackend.Instance.FactoryInventoryBackend.Index();
            var ShopTruck = new ShopTruckModel();
            var ItemId = InventoryList.FirstOrDefault().Id;

            ShopTruck.Truck = null;
            ShopTruck.Wheels = null;
            ShopTruck.Topper = null;
            ShopTruck.Trailer = null;
            ShopTruck.Menu = null;
            ShopTruck.Sign = null;

            // Act
            var result = new ShopTruckViewModel(ShopTruck);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Wheels0.png", result.Wheels, "Wheels " + TestContext.TestName);
            Assert.AreEqual("Truck0.png", result.Truck, "Truck "+TestContext.TestName);
            Assert.AreEqual("Topper0.png", result.Topper, "Topper "+TestContext.TestName);
            Assert.AreEqual("Trailer0.png", result.Trailer, "Trailer " + TestContext.TestName);
            Assert.AreEqual("Menu0.png", result.Menu, "Menu " + TestContext.TestName);
            Assert.AreEqual("Sign0.png", result.Sign, "Sign " + TestContext.TestName);
        }
        #endregion Instantiate
    }
}
