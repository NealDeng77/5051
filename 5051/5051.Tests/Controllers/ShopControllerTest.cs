using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Controllers;
using _5051.Models;
using _5051.Backend;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class ShopControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_Shop_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new AdminController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new AdminController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate


        #region IndexRegion

        [TestMethod]
        public void Controller_Shop_Index_Default_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            ViewResult result = controller.Index(id) as ViewResult;


            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Index_With_Empty_List_Should_Return_Error_Page()
        {
            // Arrange
            ShopController controller = new ShopController();

            // Set unitesting backend data
            DataSourceBackend.Instance.SetDataSourceDataSet(DataSourceDataSetEnum.UnitTest);

            // Make empty StudentList
            while (DataSourceBackend.Instance.StudentBackend.Index().Count != 0)
            {
                var first = DataSourceBackend.Instance.StudentBackend.GetDefault();
                DataSourceBackend.Instance.StudentBackend.Delete(first.Id);
            }

            // Act
            var result = (RedirectToRouteResult)controller.Index();

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion IndexRegion


        #region BuyRegion
        [TestMethod]
        public void Controller_Shop_Buy_Default_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            ViewResult result = controller.Buy(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Buy_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            ShopBuyViewModel data = new ShopBuyViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Buy(data) as ViewResult;

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }
        
        [TestMethod]
        public void Controller_Shop_Buy_Get_myDataIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            ShopController controller = new ShopController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Act
            var result = (RedirectToRouteResult)controller.Buy(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Buy_Data_Invalid_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            ShopBuyViewModel data;
            data = null;

            // Act
            var result = (RedirectToRouteResult)controller.Buy(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Buy_Data_Invalid_StudentID_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = null;

            // Act
            var result = (RedirectToRouteResult)controller.Buy(data);

            // Assert
            Assert.AreEqual("Buy", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Buy_Data_Invalid_ItemId_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = "studentID";
            data.ItemId = null;

            // Act
            var result = (RedirectToRouteResult)controller.Buy(data);

            // Assert
            Assert.AreEqual("Buy", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Buy_Data_Invalid_StudentId_Bogus_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = "bogus";
            data.ItemId = "itemID";

            // Act
            var result = (RedirectToRouteResult)controller.Buy(data);

            // Assert
            Assert.AreEqual("Buy", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Buy_Data_Invalid_ItemId_Bogus_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            data.ItemId = "bogus";

            // Act
            var result = (RedirectToRouteResult)controller.Buy(data);

            // Assert
            Assert.AreEqual("Buy", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Buy_Data_Valid_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            data.ItemId = DataSourceBackend.Instance.ShopInventoryBackend.GetFirstShopInventoryId();

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 1000;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.ShopInventoryBackend.Read(data.ItemId);

            myInventory.Tokens = 10;
            DataSourceBackend.Instance.ShopInventoryBackend.Update(myInventory);

            var expect = myStudent.Tokens - myInventory.Tokens;

            // Act
            ViewResult result = controller.Buy(data) as ViewResult;

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Buy_Data_InValid_Tokens_Not_Enough_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            data.ItemId = DataSourceBackend.Instance.ShopInventoryBackend.GetFirstShopInventoryId();

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 10;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.ShopInventoryBackend.Read(data.ItemId);

            myInventory.Tokens = 100;
            DataSourceBackend.Instance.ShopInventoryBackend.Update(myInventory);

            // No purchage, so tokens stay the same
            var expect = myStudent.Tokens;
            var expectCount = myStudent.Inventory.Count();

            // Act
            var result = (RedirectToRouteResult)controller.Buy(data);

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Buy", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCount, myStudent2.Inventory.Count(), TestContext.TestName);
        }


        [TestMethod]
        public void Controller_Shop_Buy_Data_InValid_Item_Already_Exists_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            data.ItemId = DataSourceBackend.Instance.ShopInventoryBackend.GetFirstShopInventoryId();

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 1000;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.ShopInventoryBackend.Read(data.ItemId);

            myInventory.Tokens = 10;
            DataSourceBackend.Instance.ShopInventoryBackend.Update(myInventory);

            // Buy it one time, this puts the item in the student inventory
            var myPurchage1 = (RedirectToRouteResult)controller.Buy(data);

            // No purchage, so tokens stay the same
            var expect = myStudent.Tokens;
            var expectCount = myStudent.Inventory.Count();

            // Act

            // Trying to buy the second time will fail
            var result = (RedirectToRouteResult)controller.Buy(data);

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Buy", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCount, myStudent2.Inventory.Count(), TestContext.TestName);
        }
        #endregion BuyRegion

        #region DiscountsRegion

        [TestMethod]
        public void Controller_Shop_Discounts_Default_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();

            // Act
            ViewResult result = controller.Discounts() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion DiscountsRegion

        #region SpecialsRegion

        [TestMethod]
        public void Controller_Shop_Specials_Default_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();

            // Act
            ViewResult result = controller.Specials() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion SpecialsRegion
    }
}
