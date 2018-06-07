using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    public class ShopController : Controller
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