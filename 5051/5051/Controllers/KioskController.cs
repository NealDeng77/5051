using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;
using System;

namespace _5051.Controllers
{
    /// <summary>
    /// The Kiosk that will run in the classroom
    /// </summary>
    public class KioskController : Controller
    {
        // A ViewModel used for the Student that contains the StudentList
        private StudentViewModel StudentViewModel = new StudentViewModel();

        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;

        /// <summary>
        /// Return the list of students with the status of logged in or out
        /// </summary>
        /// <returns></returns>
        // GET: Kiosk
        public ActionResult Index()
        {
            var myDataList = StudentBackend.Index();
            if (myDataList == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }
            var StudentViewModel = new StudentViewModel(myDataList);
            return View(StudentViewModel);
        }

        // GET: Kiosk/SetLogout/5
        /// <summary>
        /// Manages the Login action, toggles the state
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns></returns>
        public ActionResult SetLogin(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            StudentBackend.ToggleStatusById(id);
            return RedirectToAction("ConfirmLogin", "Kiosk", new { id });
        }

        // GET: Kiosk/SetLogout/5
        /// <summary>
        /// Manages the logout action, toggles the state
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns></returns>
        public ActionResult SetLogout(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            StudentBackend.ToggleStatusById(id);
            return RedirectToAction("ConfirmLogout","Kiosk", new { id });
        }

        /// <summary>
        /// Shows the login confirmation screen
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns></returns>
        public ActionResult ConfirmLogin(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            var myDataList = StudentBackend.Read(id);
            var StudentViewModel = new StudentDisplayViewModel(myDataList);

            //Todo, replace with actual transition time
            StudentViewModel.LastDateTime = DateTime.Now;

            return View(StudentViewModel);
        }

        /// <summary>
        /// Shows the login confirmation screen
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns></returns>
        public ActionResult ConfirmLogout(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            var myDataList = StudentBackend.Read(id);
            var StudentViewModel = new StudentDisplayViewModel(myDataList);

            //Todo, replace with actual transition time
            StudentViewModel.LastDateTime = DateTime.Now;

            return View(StudentViewModel);
        }
    }
}
