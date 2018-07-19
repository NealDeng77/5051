using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    /// <summary>
    /// FactoryInventory Contoller manages the FactoryInventory web pages including how to make new ones, change them, and delete them
    /// </summary>
    public class FactoryInventoryController : BaseController
    {
        // A ViewModel used for the FactoryInventory that contains the FactoryInventoryList
        private FactoryInventoryViewModel FactoryInventoryViewModel = new FactoryInventoryViewModel();

        // The Backend Data source
        private FactoryInventoryBackend FactoryInventoryBackend = FactoryInventoryBackend.Instance;

        // GET: FactoryInventory
        /// <summary>
        /// Index, the page that shows all the FactoryInventorys
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Load the list of data into the FactoryInventoryList
            FactoryInventoryViewModel.FactoryInventoryList = FactoryInventoryBackend.Index();
            return View(FactoryInventoryViewModel);
        }

        /// <summary>
        /// Read information on a single FactoryInventory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: FactoryInventory/Details/5
        public ActionResult Read(string id = null)
        {
            var myData = FactoryInventoryBackend.Read(id);
            return View(myData);
        }

        /// <summary>
        /// This opens up the make a new FactoryInventory screen
        /// </summary>
        /// <returns></returns>
        // GET: FactoryInventory/Create
        public ActionResult Create()
        {
            var myData = new FactoryInventoryModel();
            return View(myData);
        }

        /// <summary>
        /// Make a new FactoryInventory sent in by the create FactoryInventory screen
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        // POST: FactoryInventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Description,"+
                                        "Uri,"+
                                        "Tokens,"+
                                        "Category,"+
                                        "")] FactoryInventoryModel data)
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

            FactoryInventoryBackend.Create(data);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This will show the details of the FactoryInventory to update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: FactoryInventory/Edit/5
        public ActionResult Update(string id = null)
        {
            var myData = FactoryInventoryBackend.Read(id);
            return View(myData);
        }

        /// <summary>
        /// This updates the FactoryInventory based on the information posted from the udpate page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: FactoryInventory/Update/5
        [HttpPost]
        public ActionResult Update([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Description,"+
                                        "Uri,"+
                                        "Tokens,"+
                                        "Category,"+
                                        "")] FactoryInventoryModel data)
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

            FactoryInventoryBackend.Update(data);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This shows the FactoryInventory info to be deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: FactoryInventory/Delete/5
        public ActionResult Delete(string id = null)
        {
            var myData = FactoryInventoryBackend.Read(id);
            return View(myData);
        }

        /// <summary>
        /// This deletes the FactoryInventory sent up as a post from the FactoryInventory delete page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: FactoryInventory/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Description,"+
                                        "Uri,"+
                                        "Tokens,"+
                                        "Category,"+
                                        "")] FactoryInventoryModel data)
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

            FactoryInventoryBackend.Delete(data.Id);

            return RedirectToAction("Index");
        }
    }
}
