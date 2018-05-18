using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    /// <summary>
    /// Controller for the Admin section of the website
    /// </summary>
    public class AdminController : Controller
    {
        // A ViewModel used for the Student that contains the StudentList
        private StudentViewModel StudentViewModel = new StudentViewModel();

        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Reports
        /// </summary>
        /// <returns>All the Students that can have a report</returns>
        // GET: Report
        public ActionResult Report()
        {
            // Load the list of data into the StudentList
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            return View(StudentViewModel);
        }

        // GET: Report
        /// <summary>
        /// Returns an individual report for a student
        /// </summary>
        /// <param name="id">Student ID to generate Report for</param>
        /// <returns>Report data</returns>
        public ActionResult StudentReport(string id=null)
        {

            return View();
        }

        /// <summary>
        /// Calendar
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult Calendar()
        {
            return View();
        }

        /// <summary>
        /// Attendance Editing
        /// </summary>
        /// <returns></returns>
        // GET: Attendance
        public ActionResult Attendance()
        {
            return View();
        }
    }
}