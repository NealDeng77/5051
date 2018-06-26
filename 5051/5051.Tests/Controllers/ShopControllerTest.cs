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


        //#region IndexRegion

        //[TestMethod]
        //public void Controller_Shop_Index_Default_Should_Pass()
        //{
        //    // Arrange
        //    ShopController controller = new ShopController();

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;
            

        //    // Assert
        //    Assert.IsNotNull(result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Controller_Shop_Index_With_Empty_List_Should_Return_Error_Page()
        //{
        //    // Arrange
        //    ShopController controller = new ShopController();

        //    // Set unitesting backend data
        //    DataSourceBackend.Instance.SetDataSourceDataSet(DataSourceDataSetEnum.UnitTest);

        //    // Make empty StudentList
        //    while (DataSourceBackend.Instance.StudentBackend.Index().Count != 0)
        //    {
        //        var first = DataSourceBackend.Instance.StudentBackend.GetDefault();
        //        DataSourceBackend.Instance.StudentBackend.Delete(first.Id);
        //    }

        //    // Act
        //    var result = (RedirectToRouteResult)controller.Index();

        //    // Reset DataSourceBackend
        //    DataSourceBackend.Instance.Reset();

        //    // Assert
        //    Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        //}

        //#endregion IndexRegion


        /*************************************************

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
                Assert.IsNotNull(result, TestContext.TestName);
            }

        *********************************************************/


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
