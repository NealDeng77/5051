using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    /// <summary>
    /// ShopInventory Contoller manages the ShopInventory web pages including how to make new ones, change them, and delete them
    /// </summary>
    public class ShopInventoryController : BaseController
    {
        // A ViewModel used for the ShopInventory that contains the ShopInventoryList
        private ShopInventoryViewModel ShopInventoryViewModel = new ShopInventoryViewModel();

        // The Backend Data source
        private ShopInventoryBackend ShopInventoryBackend = ShopInventoryBackend.Instance;

        // GET: ShopInventory
        /// <summary>
        /// Index, the page that shows all the ShopInventorys
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Load the list of data into the ShopInventoryList
            ShopInventoryViewModel.ShopInventoryList = ShopInventoryBackend.Index();
            return View(ShopInventoryViewModel);
        }

        /// <summary>
        /// Read information on a single ShopInventory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: ShopInventory/Details/5
        public ActionResult Read(string id = null)
        {
            var myData = ShopInventoryBackend.Read(id);
            return View(myData);
        }

        /// <summary>
        /// This opens up the make a new ShopInventory screen
        /// </summary>
        /// <returns></returns>
        // GET: ShopInventory/Create
        public ActionResult Create()
        {
            var myData = new ShopInventoryModel();
            return View(myData);
        }

        /// <summary>
        /// Make a new ShopInventory sent in by the create ShopInventory screen
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        // POST: ShopInventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Description,"+
                                        "Uri,"+
                                        "Tokens,"+
                                        "Category,"+
                                        "")] ShopInventoryModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit, with Error Message
                return View(data);
            }

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Sind back for Edit
                return View(data);
            }

            ShopInventoryBackend.Create(data);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This will show the details of the ShopInventory to update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: ShopInventory/Edit/5
        public ActionResult Update(string id = null)
        {
            var myData = ShopInventoryBackend.Read(id);
            return View(myData);
        }

        /// <summary>
        /// This updates the ShopInventory based on the information posted from the udpate page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: ShopInventory/Update/5
        [HttpPost]
        public ActionResult Update([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Description,"+
                                        "Uri,"+
                                        "Tokens,"+
                                        "Category,"+
                                        "")] ShopInventoryModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }

            if (data == null)
            {
                // Send to error page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for Edit
                return View(data);
            }

            ShopInventoryBackend.Update(data);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This shows the ShopInventory info to be deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: ShopInventory/Delete/5
        public ActionResult Delete(string id = null)
        {
            var myData = ShopInventoryBackend.Read(id);
            return View(myData);
        }

        /// <summary>
        /// This deletes the ShopInventory sent up as a post from the ShopInventory delete page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: ShopInventory/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Description,"+
                                        "Uri,"+
                                        "Tokens,"+
                                        "Category,"+
                                        "")] ShopInventoryModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for Edit
                return View(data);
            }

            ShopInventoryBackend.Delete(data.Id);

            return RedirectToAction("Index");
        }
    }
}
