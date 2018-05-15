using System;
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
    public class AvatarControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Controller_Avatar_Index_Default_Should_Pass()
        {
            // Arrange
            AvatarController controller = new AvatarController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result,TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Read_ID_Null_Should_Return_Error()
        {
            // Arrange
            AvatarController controller = new AvatarController();
            string id = null;

            // Act
            ViewResult result = controller.Read(id) as ViewResult;

            // Assert
            Assert.AreEqual(null, result.Model, TestContext.TestName);
        }


        [TestMethod]
        public void Controller_Avatar_Read_ID_Bogus_Should_Return_Error()
        {
            // Arrange
            AvatarController controller = new AvatarController();
            string id = "bogus";

            // Act
            ViewResult result = controller.Read(id) as ViewResult;

            // Assert
            Assert.AreEqual(null, result.Model, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Read_ID_Valid_Should_Return_Error()
        {
            // Arrange
            AvatarController controller = new AvatarController();
            
            string id = AvatarBackend.Instance.GetFirstAvatarId();

            // Act
            ViewResult result = controller.Read(id) as ViewResult;

            var resultAvatar = result.Model as AvatarModel;

            // Assert
            Assert.AreEqual(id, resultAvatar.Id, TestContext.TestName);
        }



    //    public ActionResult Create()
    //    {
    //        var myData = new AvatarModel();
    //        return View(myData);
    //    }

    //    public ActionResult Create([Bind(Include=
    //                                    "Id,"+
    //                                    "Name,"+
    //                                    "Description,"+
    //                                    "Uri,"+
    //                                    "")] AvatarModel data)

    //    public ActionResult Update(string id = null)
    //    {
    //        var myData = avatarBackend.Read(id);
    //        return View(myData);
    //    }

    //    public ActionResult Update([Bind(Include=
    //                                    "Id,"+
    //                                    "Name,"+
    //                                    "Description,"+
    //                                    "Uri,"+
    //                                    "")] AvatarModel data)

    //    public ActionResult Delete(string id = null)
    //    {
    //        var myData = avatarBackend.Read(id);
    //        return View(myData);
    //    }

    //    [HttpPost]
    //    public ActionResult Delete([Bind(Include=
    //                                    "Id,"+
    //                                    "Name,"+
    //                                    "Description,"+
    //                                    "Uri,"+
    //                                    "")] AvatarModel data)

    }
}
