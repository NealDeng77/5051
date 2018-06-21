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

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class KioskControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_Kiosk_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new KioskController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new KioskController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region IndexRegion
        [TestMethod]
        public void Controller_Kiosk_Index_Default_Should_Pass()
        {
            // Arrange
            KioskController controller = new KioskController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_Index_With_Empty_List_Should_Return_Error_Page()
        {
            // Arrange
            KioskController controller = new KioskController();
            
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

        #region SetLoginRegion
        [TestMethod]
        public void Controller_Kiosk_SetLogin_Valid_Id_Should_Pass()
        {
            // Arrange
            var controller = new KioskController();

            // Get the first Studnet Id from the DataSource
            string id = StudentBackend.Instance.GetDefault().Id;

            // Act
            var result = (RedirectToRouteResult)controller.SetLogin(id);

            // check status change after SetLogin
            var resultStatus = StudentBackend.Instance.Read(id).Status;
            Assert.AreEqual(StudentStatusEnum.In, resultStatus, TestContext.TestName);

            // Assert
            Assert.AreEqual("ConfirmLogin", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_SetLogin_Null_Or_Empty_Id_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new KioskController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.SetLogin(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }
        #endregion SetLoginRegion

        #region SetLogoutRegion
        [TestMethod]
        public void Controller_Kiosk_SetLogout_Valid_Id_Should_Pass()
        {
            // Arrange
            var controller = new KioskController();

            // Get the first Studnet Id from the DataSource
            string id = StudentBackend.Instance.GetDefault().Id;

            // Act
            var result = (RedirectToRouteResult)controller.SetLogout(id);

            // check status change after SetLogout
            var resultStatus = StudentBackend.Instance.Read(id).Status;
            Assert.AreEqual(StudentStatusEnum.Out, resultStatus, TestContext.TestName);

            // Assert
            Assert.AreEqual("ConfirmLogout", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_SetLogout_Null_Or_Empty_Id_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new KioskController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.SetLogout(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }
        #endregion SetLogoutRegion

        //#region ConfirmLogin
        //[TestMethod]
        //public void Controller_Kiosk_ConfirmLogin_Valid_Id_Should_Pass()
        //{
        //    // Arrange
        //    var controller = new KioskController();
        //    string id = StudentBackend.Instance.GetDefault().Id;

        //    // Act
        //    ViewResult result = controller.ConfirmLogin(id) as ViewResult;



        //}
        //#endregion ConfirmLogin

        //    public ActionResult ConfirmLogin(string id)
        //    {
        //        if (string.IsNullOrEmpty(id))
        //        {
        //            return RedirectToAction("Error", "Home");
        //        }

        //        var myDataList = StudentBackend.Read(id);
        //        var StudentViewModel = new StudentDisplayViewModel(myDataList);

        //        //Todo, replace with actual transition time
        //        StudentViewModel.LastDateTime = DateTime.Now;

        //        return View(StudentViewModel);
        //    }
    }
}