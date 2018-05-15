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

        #region IndexRegion
        [TestMethod]
        public void Controller_Avatar_Index_Default_Should_Pass()
        {
            // Arrange
            AvatarController controller = new AvatarController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion IndexRegion

        #region ReadRegion
        [TestMethod]
        public void Controller_Avatar_Read_ID_Null_Should_Return_Empty_Model()
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
        public void Controller_Avatar_Read_ID_Bogus_Should_Return_Empty_Model()
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
        public void Controller_Avatar_Read_ID_Valid_Should_Pass()
        {
            // Arrange
            AvatarController controller = new AvatarController();

            // Get the first Avatar from the DataSource
            string id = AvatarBackend.Instance.GetFirstAvatarId();

            // Act
            ViewResult result = controller.Read(id) as ViewResult;

            var resultAvatar = result.Model as AvatarModel;

            // Assert
            Assert.AreEqual(id, resultAvatar.Id, TestContext.TestName);
        }

        #endregion ReadRegion

        #region CreateRegion
        [TestMethod]
        public void Controller_Avatar_Create_Get_Should_Return_New_Model()
        {
            // Arrange
            AvatarController controller = new AvatarController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            var resultAvatar = result.Model as AvatarModel;

            // Assert
            Assert.AreNotEqual(null, resultAvatar.Id, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Create_Post_Invalid_Null_Id_Should_Return_Model()
        {
            // Arrange
            AvatarController controller = new AvatarController();
            var data = new AvatarModel
            {
                Description = "description",
                Id = null,
                Name = "Name",
                Uri = "picture"
            };

            // Act
            ViewResult result = controller.Create(data) as ViewResult;

            var resultAvatar = result.Model as AvatarModel;

            // Assert
            Assert.AreEqual(data.Description, resultAvatar.Description, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Create_Post_Valid_Should_Return_Index_Page()
        {
            // Arrange
            AvatarController controller = new AvatarController();

            var data = new AvatarModel
            {
                Description = "description",
                Id = "abc",
                Name = "Name",
                Uri = "picture"
            };

            // Act
            var result = (RedirectToRouteResult)controller.Create(data);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
            // No need to check the route, Assert.AreEqual("Avatar", result.RouteValues["route"], TestContext.TestName);

            // Check that the item is created
            var resultAvatar = AvatarBackend.Instance.Read("abc");
            Assert.AreEqual(data.Id, resultAvatar.Id, TestContext.TestName);

            // Reset the Avatars
            AvatarBackend.Instance.Reset();

        }

        [TestMethod]
        public void Controller_Avatar_Create_Post_InValid_Should_Return_Error_Page()
        {
            /// <summary>
            /// This Test calls the create, but passes null data
            /// The controller will return a redirect to the error home page
            /// So the test needs to cast the return to a redirect, and then check that it got to the home error page
            /// </summary>

            // Arrange
            AvatarController controller = new AvatarController();

            // Act
            var result = (RedirectToRouteResult)controller.Create(null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["route"], TestContext.TestName);
        }
        #endregion CreateRegion

        #region UpdateRegion
        //[TestMethod]
        //public void Controller_Avatar_Update_Get_Should_Return_New_Model()
        //{
        //    // Arrange
        //    AvatarController controller = new AvatarController();

        //    // Act
        //    ViewResult result = controller.Update() as ViewResult;

        //    var resultAvatar = result.Model as AvatarModel;

        //    // Assert
        //    Assert.AreNotEqual(null, resultAvatar.Id, TestContext.TestName);
        //}

        [TestMethod]
        public void Controller_Avatar_Update_Post_Invalid_Null_Id_Should_Return_Model()
        {
            // Arrange
            AvatarController controller = new AvatarController();
            var data = new AvatarModel
            {
                Description = "description",
                Id = null,
                Name = "Name",
                Uri = "picture"
            };

            // Act
            ViewResult result = controller.Update(data) as ViewResult;

            var resultAvatar = result.Model as AvatarModel;

            // Assert
            Assert.AreEqual(data.Description, resultAvatar.Description, TestContext.TestName);
        }

        //[TestMethod]
        //public void Controller_Avatar_Update_Post_Valid_Should_Return_Index_Page()
        //{
        //    // Arrange
        //    AvatarController controller = new AvatarController();

        //    var data = new AvatarModel
        //    {
        //        Description = "description",
        //        Id = "abc",
        //        Name = "Name",
        //        Uri = "picture"
        //    };

        //    // Act
        //    var result = (RedirectToRouteResult)controller.Update(data);

        //    // Assert
        //    Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        //    // No need to check the route, Assert.AreEqual("Avatar", result.RouteValues["route"], TestContext.TestName);

        //    // Check that the item is Updated
        //    var resultAvatar = AvatarBackend.Instance.Read("abc");
        //    Assert.AreEqual(data.Id, resultAvatar.Id, TestContext.TestName);

        //    // Reset the Avatars
        //    AvatarBackend.Instance.Reset();

        //}

        [TestMethod]
        public void Controller_Avatar_Update_Post_InValid_Should_Return_Error_Page()
        {
            /// <summary>
            /// This Test calls the Update, but passes null data
            /// The controller will return a redirect to the error home page
            /// So the test needs to cast the return to a redirect, and then check that it got to the home error page
            /// </summary>

            // Arrange
            AvatarController controller = new AvatarController();

            // Act
            var result = (RedirectToRouteResult)controller.Update((AvatarModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["route"], TestContext.TestName);
        }
        #endregion UpdateRegion


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
