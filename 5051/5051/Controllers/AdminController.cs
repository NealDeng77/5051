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
        public ActionResult StudentReport(string id = null)
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

        /// <summary>
        /// Settings page for the admin, allows for data reset, switching between data modes etc.
        /// </summary>
        /// <returns></returns>
        // GET: Settings
        public ActionResult Settings()
        {
            return View();
        }

        /// <summary>
        /// Calls the data sources and has them reset to default data
        /// </summary>
        /// <returns></returns>
        // GET: Reset
        public ActionResult Reset()
        {
            DataSourceBackend.Instance.Reset();
            return RedirectToAction("Index", "Admin");
        }

        /// <summary>
        /// Change the data set from default to demo, to ut etc.
        /// </summary>
        /// <returns></returns>
        // GET: Settings
        public ActionResult DataSourceSet(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Admin");
            }

            DataSourceDataSetEnum SetEnum = DataSourceDataSetEnum.Default;
            switch (id)
            {
                case "Default":
                    SetEnum = DataSourceDataSetEnum.Default;
                    break;

                case "Demo":
                    SetEnum = DataSourceDataSetEnum.Demo;
                    break;

                case "UnitTest":
                    SetEnum = DataSourceDataSetEnum.UnitTest;
                    break;
            }

            DataSourceBackend.Instance.SetDataSourceDataSet(SetEnum);

            return RedirectToAction("Index", "Admin");
        }

        /// <summary>
        /// Change the data source
        /// </summary>
        /// <returns></returns>
        // GET: Settings
        public ActionResult DataSource(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Admin");
            }

            DataSourceEnum SetEnum = DataSourceEnum.Mock;
            switch (id)
            {
                case "Mock":
                    SetEnum = DataSourceEnum.Mock;
                    break;

                case "SQL":
                    SetEnum = DataSourceEnum.SQL;
                    break;

                case "Unknown":
                default:
                    SetEnum = DataSourceEnum.Unknown;
                    break;
            }

            DataSourceBackend.Instance.SetDataSource(SetEnum);

            return RedirectToAction("Index", "Admin");
        }
    }
}