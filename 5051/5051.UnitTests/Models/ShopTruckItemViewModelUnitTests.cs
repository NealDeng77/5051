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
    public class ShopTruckItemViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_ShopTruckItemViewModel_Default_Instantiate_Should_Pass()
        {
            // Arrange

            // Act
            var result = new ShopTruckItemViewModel(null,null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);

        }

        [TestMethod]
        public void Models_ShopTruckItemViewModel_Invalid_StudentID_Null_Should_Fail()
        {
            // Arrange

            // Act
            var result = new ShopTruckItemViewModel(null, "bogus");

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_ShopTruckItemViewModel_Invalid_ItemId_Null_Should_Fail()
        {
            // Arrange

            // Act
            var result = new ShopTruckItemViewModel("bogus", null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_ShopTruckItemViewModel_Invalid_StudentID_Bogus_ItemId_Bogus_Should_Fail()
        {
            // Arrange

            // Act
            var result = new ShopTruckItemViewModel("bogus", "Bogus");

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_ShopTruckItemViewModel_Valid_StudentID_Bogus_ItemId_Bogus_Should_Fail()
        {
            // Arrange
            var student = Backend.DataSourceBackend.Instance.StudentBackend.GetDefault();

            // Act
            var result = new ShopTruckItemViewModel(student.Id, "Bogus");

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_ShopTruckItemViewModel_Valid_StudentID_Valid_ItemId_Should_Fail()
        {
            // Arrange
            var student = Backend.DataSourceBackend.Instance.StudentBackend.GetDefault();
            var item = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Truck);

            // Act
            var result = new ShopTruckItemViewModel(student.Id, item.Id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_ShopTruckItemViewModel_Invalid_Category_Empty_Should_Fail()
        {
            // Arrange
            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var item = DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Truck);

            // Make a copy of the Item, and add it to Student Inventory and save student
            var studentItem = new FactoryInventoryModel(item);
            student.Inventory.Add(studentItem);
            DataSourceBackend.Instance.StudentBackend.Update(student);

            // Change inventory category to not match, and Save it
            item.Category = FactoryInventoryCategoryEnum.Unknown;
            DataSourceBackend.Instance.FactoryInventoryBackend.Update(item);

            // Act
            var result = new ShopTruckItemViewModel(student.Id, item.Id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_ShopTruckItemViewModel_Valid_Should_Fail()
        {
            // Arrange
            var student = Backend.DataSourceBackend.Instance.StudentBackend.GetDefault();


            var item = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Truck);

            // Add item to Student Inventory and save student
            student.Inventory.Add(item);
            Backend.DataSourceBackend.Instance.StudentBackend.Update(student);

            // Act
            var result = new ShopTruckItemViewModel(student.Id, item.Id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
