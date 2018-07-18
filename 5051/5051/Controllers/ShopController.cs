using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    public class ShopController : BaseController
    {
        private ShopInventoryViewModel ShopInventoryViewModel = new ShopInventoryViewModel();

        // The Backend Data source
        private ShopInventoryBackend ShopInventoryBackend = ShopInventoryBackend.Instance;

        /// <summary>
        /// Index to the Shop
        /// </summary>
        /// <returns></returns>
        // GET: Shop
        public ActionResult Index(string id = null)
        {
            // Get the Student
            var myStudent = StudentBackend.Instance.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(myStudent);
        }

        /// <summary>
        /// What to Buy at the store
        /// </summary>
        /// <returns></returns>
        // GET: Buy
        public ActionResult Buy(string id = null)
        {
            // Load the list of data into the ShopInventoryList
            var myData = new SelectedShopInventoryForStudentViewModel();

            // Get the Student
            var myStudent = StudentBackend.Instance.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }
            myData.Student = myStudent;

            // Get the Inventory
            var InventoryList = ShopInventoryBackend.Index();

            // Sort the Inventory into List per Category
            // Load the ones
            foreach (var item in Enum.GetValues(typeof(ShopInventoryCategoryEnum)))
            {
                var temp = new ShopInventoryViewModel();
                temp.Category = (ShopInventoryCategoryEnum)item;
                temp.ShopInventoryList = InventoryList.Where(m => m.Category == (ShopInventoryCategoryEnum)item).ToList();

                if (temp.ShopInventoryList.Any())
                {
                    // todo, tag the ones that are already owned
                    myData.ShopInventoryCategoryList.Add(temp);
                }
            }

            return View(myData);
        }
        [HttpPost]
        public ActionResult Buy([Bind(Include=
                                        "StudentId,"+
                                        "ItemId,"+
                                        "")] ShopBuyViewModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit, with Error Message
                return RedirectToAction("Buy", "Shop", new { id = data.StudentId });
            }

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            if (string.IsNullOrEmpty(data.StudentId))
            {
                // Send back for Edit
                return RedirectToAction("Buy", "Shop", new { id = data.StudentId });
            }

            if (string.IsNullOrEmpty(data.ItemId))
            {
                // Send back for Edit
                return RedirectToAction("Buy", "Shop", new { id = data.StudentId });
            }

            // Get Student
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            if (myStudent == null)
            {
                // Send back for Edit
                return RedirectToAction("Buy", "Shop", new { id = data.StudentId });
            }

            // Get Item
            var myItem = DataSourceBackend.Instance.ShopInventoryBackend.Read(data.ItemId);
            if (myItem == null)
            {
                // Send back for Edit
                return RedirectToAction("Buy", "Shop", new { id = data.StudentId });
            }

            // Check the Student Token amount, If not enough, return error
            if (myStudent.Tokens < myItem.Tokens)
            {
                // Not enough money...
                // Send back for Edit
                return RedirectToAction("Buy", "Shop", new { id = data.StudentId });
            }

            // Check to see if the student already has the item.  If so, don't buy again, only 1 item per student
            var ItemAlreadyExists = myStudent.Inventory.Exists(m => m.Id == myItem.Id);
            if (ItemAlreadyExists)
            {
                // Already own it.
                return RedirectToAction("Buy", "Shop", new { id = data.StudentId });
            }

            // Time to buy !

            // Reduce the Student Tokens by Item Price
            myStudent.Tokens -= myItem.Tokens;

            // Add Item to Student Inventory
            // TODO:  Mike, add inventory to Students...
            myStudent.Inventory.Add(myItem);

            // Update Student
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            return RedirectToAction("Buy", "Shop", new { id = data.StudentId });
        }

        /// <summary>
        /// Things on sale at the store
        /// </summary>
        /// <returns></returns>
        // GET: Discounts
        public ActionResult Discounts()
        {
            return View();
        }

        /// <summary>
        /// Unique items to get at the store
        /// </summary>
        /// <returns></returns>
        // GET: Specials
        public ActionResult Specials()
        {
            return View();
        }

        
    }
}