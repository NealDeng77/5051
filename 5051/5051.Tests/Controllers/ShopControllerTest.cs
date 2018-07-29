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


        #region FactoryRegion
        [TestMethod]
        public void Controller_Shop_Factory_Default_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            ViewResult result = controller.Factory(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            ShopBuyViewModel data = new ShopBuyViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Factory(data) as ViewResult;

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }
        
        [TestMethod]
        public void Controller_Shop_Factory_Get_myDataIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            ShopController controller = new ShopController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Act
            var result = (RedirectToRouteResult)controller.Factory(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_Invalid_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            ShopBuyViewModel data;
            data = null;

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_Invalid_StudentID_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = null;

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_Invalid_ItemId_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = "studentID";
            data.ItemId = null;

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_Invalid_StudentId_Bogus_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = "bogus";
            data.ItemId = "itemID";

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_Invalid_ItemId_Bogus_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            data.ItemId = "bogus";

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_Valid_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            data.ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetFirstFactoryInventoryId();

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 1000;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.FactoryInventoryBackend.Read(data.ItemId);

            myInventory.Tokens = 10;
            DataSourceBackend.Instance.FactoryInventoryBackend.Update(myInventory);

            var expect = myStudent.Tokens - myInventory.Tokens;

            // Act
            ViewResult result = controller.Factory(data) as ViewResult;

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_InValid_Tokens_Not_Enough_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            data.ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetFirstFactoryInventoryId();

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 10;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.FactoryInventoryBackend.Read(data.ItemId);

            myInventory.Tokens = 100;
            DataSourceBackend.Instance.FactoryInventoryBackend.Update(myInventory);

            // No purchage, so tokens stay the same
            var expect = myStudent.Tokens;
            var expectCount = myStudent.Inventory.Count();

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCount, myStudent2.Inventory.Count(), TestContext.TestName);
        }


        [TestMethod]
        public void Controller_Shop_Factory_Data_InValid_Item_Already_Exists_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            data.ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetFirstFactoryInventoryId();

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 1000;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.FactoryInventoryBackend.Read(data.ItemId);

            myInventory.Tokens = 10;
            DataSourceBackend.Instance.FactoryInventoryBackend.Update(myInventory);

            // Buy it one time, this puts the item in the student inventory
            var myPurchage1 = (RedirectToRouteResult)controller.Factory(data);

            // No purchage, so tokens stay the same
            var expect = myStudent.Tokens;
            var expectCount = myStudent.Inventory.Count();

            // Act

            // Trying to buy the second time will fail
            var result = (RedirectToRouteResult)controller.Factory(data);

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCount, myStudent2.Inventory.Count(), TestContext.TestName);
        }
        #endregion FactoryRegion

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

        #region VisitRegion
        [TestMethod]
        public void Controller_Shop_Visit_Valid_Should_Return_List()
        {
            // Arrange
            ShopController controller = new ShopController();
            var expect = Backend.StudentBackend.Instance.Index();

            //// Act
            var resultCall = controller.Visit() as ViewResult;
            var result = (List<StudentModel>)resultCall.Model;

            //// Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion VisitRegion

        #region VisitShopRegion
        [TestMethod]
        public void Controller_Shop_VisitShop_Invalid_Null_Id_Should_Return_RosterPage()
        {
            //// Arrange
            ShopController controller = new ShopController();
            string id = null;

            //// Act
            var result = (RedirectToRouteResult)controller.VisitShop(id);

            //// Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Visit_Invalid_ID_Should_Fail()
        {
            //// Arrange
            ShopController controller = new ShopController();
            string id = "bogus";

            //// Act
            var result = (RedirectToRouteResult)controller.VisitShop(id);

            //// Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Visit_Valid_Id_Should_Pass()
        {
            //// Arrange
            ShopController controller = new ShopController();
            var data = Backend.StudentBackend.Instance.GetDefault();
            var expect = data.Name;

            //// Act
            var resultCall = controller.VisitShop(data.Id) as ViewResult;
            var result = (StudentModel)resultCall.Model;

            //// Assert
            Assert.AreEqual(expect, result.Name, TestContext.TestName);
        }
        #endregion VisitShop

        #region CelebrityPoster
        [TestMethod]
        public void Controller_Shop_CelebrityPoster_Default_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();

            // Act
            ViewResult result = controller.CelebrityPoster() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion CelebrityPoster

        #region Edit
        [TestMethod]
        public void Controller_Shop_Edit_Invalid_No_StudentID_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            // Act
            var result = controller.Edit() as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Invalid_Null_StudentID_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            // Act
            var result = controller.Edit(null) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Invalid_StudentID_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            // Act
            var result = controller.Edit("bogus") as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Valid_StudentID_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();

            // Act
            var result = controller.Edit(data.Id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Valid_Truck_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Truck = null;

            // Act
            var result = controller.Edit(data.Id) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }
        #endregion Edit

        #region Inventory
        [TestMethod]
        public void Controller_Shop_Inventory_Default_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            ViewResult result = controller.Inventory(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Inventory_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            ShopBuyViewModel data = new ShopBuyViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Inventory(data) as ViewResult;

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Inventory_Get_myDataIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            ShopController controller = new ShopController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Act
            var result = (RedirectToRouteResult)controller.Inventory(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Inventory_Data_Invalid_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            ShopBuyViewModel data;
            data = null;

            // Act
            var result = (RedirectToRouteResult)controller.Inventory(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Inventory_Data_Invalid_StudentID_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = null;

            // Act
            var result = (RedirectToRouteResult)controller.Inventory(data);

            // Assert
            Assert.AreEqual("Inventory", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Inventory_Data_Invalid_ItemId_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = "studentID";
            data.ItemId = null;

            // Act
            var result = (RedirectToRouteResult)controller.Inventory(data);

            // Assert
            Assert.AreEqual("Inventory", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Inventory_Data_Invalid_StudentId_Bogus_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = "bogus";
            data.ItemId = "itemID";

            // Act
            var result = (RedirectToRouteResult)controller.Inventory(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Inventory", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Inventory_Data_Invalid_ItemId_Bogus_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            data.ItemId = "bogus";

            // Act
            var result = (RedirectToRouteResult)controller.Inventory(data);

            // Assert
            Assert.AreEqual("Inventory", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Inventory_Data_Valid_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            data.ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetFirstFactoryInventoryId();

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 1000;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.FactoryInventoryBackend.Read(data.ItemId);

            myInventory.Tokens = 10;
            DataSourceBackend.Instance.FactoryInventoryBackend.Update(myInventory);

            var expect = myStudent.Tokens - myInventory.Tokens;

            // Act
            ViewResult result = controller.Factory(data) as ViewResult;

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Inventory_Data_InValid_Tokens_Not_Enough_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            data.ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetFirstFactoryInventoryId();

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 10;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.FactoryInventoryBackend.Read(data.ItemId);

            myInventory.Tokens = 100;
            DataSourceBackend.Instance.FactoryInventoryBackend.Update(myInventory);

            // No purchage, so tokens stay the same
            var expect = myStudent.Tokens;
            var expectCount = myStudent.Inventory.Count();

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCount, myStudent2.Inventory.Count(), TestContext.TestName);
        }


        [TestMethod]
        public void Controller_Shop_Inventory_Data_InValid_Item_Already_Exists_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel();
            data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            data.ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetFirstFactoryInventoryId();

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 1000;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.FactoryInventoryBackend.Read(data.ItemId);

            myInventory.Tokens = 10;
            DataSourceBackend.Instance.FactoryInventoryBackend.Update(myInventory);

            // Buy it one time, this puts the item in the student inventory
            var myPurchage1 = (RedirectToRouteResult)controller.Factory(data);

            // No purchage, so tokens stay the same
            var expect = myStudent.Tokens;
            var expectCount = myStudent.Inventory.Count();

            // Act

            // Trying to buy the second time will fail
            var result = (RedirectToRouteResult)controller.Factory(data);

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCount, myStudent2.Inventory.Count(), TestContext.TestName);
        }
        #endregion Inventory
    }
}
